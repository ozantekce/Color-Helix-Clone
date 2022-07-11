 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    public static bool isGameOver;

    private MeshRenderer meshRenderer;
    private SpriteRenderer splash;

    private float lerpAmount;

    private float height = 0.58f, speed = 250;

    private bool move,isRising;

    public bool perfectStar;

    public bool displayed;


    private AudioSource failSound;
    private AudioSource hitSound;
    private AudioSource levelCompleteSound;

    public static float Z { get => z; set => z = value; }
    public static Color CurrentColor { get => currentColor; set => currentColor = value; }

    private void Awake()
    {
        
        failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        levelCompleteSound = GameObject.Find("LevelCompleteSound").GetComponent<AudioSource>();

        meshRenderer = GetComponent<MeshRenderer>();
        splash = transform.GetChild(0).GetComponent<SpriteRenderer>();


        destroyWallList = new List<GameObject>();

    }


    void Start()
    {

        CurrentColor = GameController.Instance.hitColor;
        move = false;
    }

    void Update()
    {

        if (isGameOver)
            return;

        if (Touch.IsPressing())
        {
            move = true;
        }

        if (move)
        {
            Ball.z += speed * 0.025f * Time.deltaTime;
        }

        transform.position = new Vector3 (0, height, Ball.z);


        displayed = false;

        UpdateColor();



    }

    void UpdateColor()
    {
        meshRenderer.sharedMaterial.color = currentColor;
        if (isRising)
        {
            currentColor = Color.Lerp(meshRenderer.material.color, hittedColorBump.Color
                ,lerpAmount);
            lerpAmount += Time.deltaTime*0.1f;
        }

        if(lerpAmount >= 1)
        {
            isRising = false;
        }

    }


    private ColorBump hittedColorBump;
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Star"))
        {
            perfectStar = true;
        }

        if (other.CompareTag("Hit"))
        {

            GameObject wall = other.transform.parent.gameObject;
            if (destroyWallList.Contains(wall))
                return;

            

            if (perfectStar && !displayed)
            {
                GameController.Instance.Score += PlayerPrefs.GetInt("Level") * 2;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay") as GameObject, transform.position,Quaternion.identity);
                pointDisplay.GetComponent<PointDisplay>().SetText("PERFECT + " + PlayerPrefs.GetInt("Level")*2);
            }
            else if(!perfectStar && !displayed)
            {
                GameController.Instance.Score += PlayerPrefs.GetInt("Level");
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay") as GameObject, transform.position, Quaternion.identity);
                pointDisplay.GetComponent<PointDisplay>().SetText("+ " + PlayerPrefs.GetInt("Level"));
            }
            perfectStar = false;
            hitSound.Play();
            StartCoroutine(DestroyWall(wall));

        }
        else if (other.CompareTag("ColorBump"))
        {
            lerpAmount = 0;
            isRising = true;
            hittedColorBump = other.GetComponent<ColorBump>();
        }
        else if (other.CompareTag("Fail") && !isGameOver)
        {
            print("We hit fail");
            StartCoroutine(GameOver());
            
        }
        else if (other.CompareTag("FinishLine"))
        {
            print("Finish");
            StartCoroutine(PlayNewLevel());
        }



    }

    private List<GameObject> destroyWallList;
    private IEnumerator DestroyWall(GameObject wall)
    {
        destroyWallList.Add(wall);

        foreach (Transform child in wall.transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddExplosionForce(10f,transform.position,0.6f,0.6f,ForceMode.VelocityChange);
            rb.transform.DOScale(0.2f, 0.5f);
        }
        
        

        yield return new WaitForSeconds(1f);
        destroyWallList.Remove(wall);
        Destroy(wall.gameObject);
    }



    IEnumerator PlayNewLevel()
    {

        levelCompleteSound.Play();
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5f);

        move = false;
        Camera.main.GetComponent<CameraFollow>().Flash();
        GameController.Instance.Level++;
        
        Ball.Z = 0;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        GameController.Instance.GenetareLevel();
        
        


    }

    
    IEnumerator GameOver()
    {

        failSound.Play();
        isGameOver = true;
        
        splash.color = currentColor;
        splash.transform.position = new Vector3(0, 0.7f, Ball.Z - 0.05f);
        splash.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        splash.enabled = true;

        meshRenderer.enabled = false;

        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        Camera.main.GetComponent<CameraFollow>().Flash();

        Ball.Z = 0;
        transform.position = new Vector3(0, 0, 0);
        GetComponent<SphereCollider>().enabled = true;
        move = false;
        GameController.Instance.GenetareLevel();
        
        splash.enabled=false;
        meshRenderer.enabled=true;
        isGameOver = false;
        

    }


}
