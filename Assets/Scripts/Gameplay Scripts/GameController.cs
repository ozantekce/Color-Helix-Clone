using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController instance;


    public GameObject finishLine;
    private GameObject[] walls2, walls1;

    private int score;

    private bool colorBump;

    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private int wallSpawnNumber = 11, wallsCount =0;
    private float z = 7;

    private float smallWallChange = 10f;

    public static GameController Instance { get => instance; set => instance = value; }
    public Helix Helix {
        get { 
            
            if(helix == null)
            {
                helix = GameObject.Find("Helix").GetComponent<Helix>();
            }
            return helix;
            
        }
        
        set => helix = value; }

    public bool ColorBump { get => colorBump; set => colorBump = value; }
    public int Level {
        get {
            return PlayerPrefs.GetInt("Level", 1);
        }
        set
        {
            PlayerPrefs.SetInt("Level", value);

        }
    
    }

    public float SmallWallChange { get
        {
            return  Mathf.Min( (15+Level)/100f,0.7f);
        }
    }

    public int WallSpawnNumber
    {
        get
        {
            return Mathf.Min((8 + Level/5),30);
        }
    }

    public int Score { get => score; set => score = value; }

    private Helix helix;

    void Awake()
    {
        instance = this;
        GenerateColors();


    }

    void Start()
    {
        
        GenetareLevel();
    }


    private void Update()
    {
        //SumUpWalls();
    }

    public void GenetareLevel()
    {

        GenerateColors();

        Debug.Log("currentLevel : "+Level);
        wallSpawnNumber = WallSpawnNumber;
        z = 7;

        DeleteWalls();
        colorBump = false;
        SpawnWalls();

    }




    void GenerateColors()
    {

        hitColor  = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];

        while(failColor == hitColor)
        {
            failColor = colors[Random.Range(0,colors.Length)];
        }

        Ball.CurrentColor = hitColor;


    }

    void SumUpWalls()
    {
        walls1 = GameObject.FindGameObjectsWithTag("Wall1");

        if (walls1.Length > wallsCount)
        {
            wallsCount = walls1.Length;

        }

        if(wallsCount > walls1.Length)
        {
            wallsCount = walls1.Length;
            if (GameObject.Find("Ball").GetComponent<Ball>().perfectStar)
            {
                GameObject.Find("Ball").GetComponent<Ball>().perfectStar = false;
                score += PlayerPrefs.GetInt("Level") * 2;

            }
            else
            {
                score += PlayerPrefs.GetInt("Level");
            }
            print(Score);
        }

    }


    void DeleteWalls()
    {

        GameObject helix = GameObject.Find("Helix");

        foreach (Transform child in helix.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Destroy(GameObject.FindGameObjectWithTag("ColorBump"));


    }


    void SpawnWalls()
    {
        GameObject wall;
        for (int i = 0; i < wallSpawnNumber; i++)
        {
            
            
            if(Random.value<=0.2f && !colorBump)
            {
                colorBump = true;
                wall = GameObject.Instantiate(Resources.Load("ColorBump") as GameObject, transform.position,Quaternion.identity);
            }
            else if(Random.value<=0.2)
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            }
            else if(i>= wallSpawnNumber-1 && !colorBump)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }

            
            

            wall.transform.SetParent(Helix.transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0f,360f);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));

            z += 7;

            //Debug.Log(z+" "+ wall.name);

        }

        finishLine.transform.position = new Vector3(0, 0.03f, z);

    }


}
