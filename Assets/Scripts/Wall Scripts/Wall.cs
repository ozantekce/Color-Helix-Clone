using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    

    private GameObject wallFragment;
    private GameObject wall1, wall2;

    private float rotationZ;
    private float rotationZMax = 180;


    private enum Difficulty
    {
        easy,medium,hard,veryHard
    }
    private class Range
    {
        public int min;
        public int max;
        public Range(int min,int max)
        {
            this.min = min;
            this.max = max;
        }
    }
    private Dictionary<Difficulty,Range> difficultyLevel;
    private Difficulty difficulty;
    void Awake()
    {
        difficulty = Difficulty.hard;

        wallFragment = Resources.Load("WallFragment") as GameObject;

        difficultyLevel = new Dictionary<Difficulty,Range>() {
            { Difficulty.easy, new Range(144,180)},
            { Difficulty.medium, new Range(108,180)},
            { Difficulty.hard, new Range(72,144)},
            { Difficulty.veryHard, new Range(36,144)},
        };

    }

    void Start()
    {
       SpawnWallFragments();
        
    }


    void SpawnWallFragments()
    {

        wall1 = new GameObject();
        wall2 = new GameObject();

        wall1.name = "Wall1";
        wall2.name = "Wall2";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);


        rotationZMax = Random.Range(difficultyLevel[difficulty].min, difficultyLevel[difficulty].max);



        for (int i = 0; i < 100; i++)
        {
            GameObject tempWallFragment = Instantiate(wallFragment, transform.position, Quaternion.Euler(0,0,rotationZ));
            rotationZ += 3.6f;

            tempWallFragment.AddComponent<BoxCollider>();
            if (rotationZ <= rotationZMax)
            {
                tempWallFragment.transform.SetParent(wall1.transform);
                tempWallFragment.gameObject.tag = "Hit";
            }
            else
            {
                tempWallFragment.transform.SetParent(wall2.transform);
                tempWallFragment.gameObject.tag = "Fail";
            }

        }

        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }



}
