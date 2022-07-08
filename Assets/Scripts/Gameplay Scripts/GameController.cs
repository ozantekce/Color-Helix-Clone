using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController instance;


    public GameObject finishLine;


    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private int wallSpawnNumber = 11;
    private float z = 7;


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

    private Helix helix;

    void Awake()
    {
        instance = this;
        GenerateColors();


    }

    void Start()
    {
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


    void SpawnWalls()
    {

        for (int i = 0; i < wallSpawnNumber; i++)
        {
            GameObject wall;
            wall = Instantiate(Resources.Load("Wall") as GameObject,transform.position,Quaternion.identity);

            wall.transform.SetParent(Helix.transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0f,360f);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
            z += Random.Range(7f,20f);
            Debug.Log(z);

        }

        finishLine.transform.position = new Vector3(0, 0.03f, z);

    }


}
