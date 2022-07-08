 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    private MeshRenderer meshRenderer;

    private float lerpAmount;

    private float height = 0.58f, speed = 300;

    private bool move,isRising;

    public static float Z { get => z; set => z = value; }
    public static Color CurrentColor { get => currentColor; set => currentColor = value; }

    private void Awake()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();

        destroyWallList = new List<GameObject>();

    }


    void Start()
    {
        move = false;
    }

    void Update()
    {

        if (Touch.IsPressing())
        {
            move = true;
        }

        if (move)
        {
            Ball.z += speed * 0.025f * Time.deltaTime;
        }

        transform.position = new Vector3 (0, height, Ball.z);


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

        if (other.CompareTag("Hit"))
        {
            GameObject wall = other.transform.parent.gameObject;
            if (!destroyWallList.Contains(wall))
                StartCoroutine(DestroyWall(wall));
        }
        else if (other.CompareTag("ColorBump"))
        {
            lerpAmount = 0;
            isRising = true;
            hittedColorBump = other.GetComponent<ColorBump>();
        }
        else if (other.CompareTag("Fail"))
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
        
        

        yield return new WaitForSeconds(0.6f);
        Destroy(wall.gameObject);
    }



    IEnumerator PlayNewLevel()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5f);

        move = false;

        //Flash
        //Levell++
        Ball.Z = 0;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        GameController.Instance.GenetareLevel();

    }

    IEnumerator GameOver()
    {
        GameController.Instance.GenetareLevel();
        Ball.Z = 0;
        move = false;
        yield break;
    }


}
