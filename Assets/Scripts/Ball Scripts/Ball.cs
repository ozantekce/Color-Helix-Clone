 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;

    private float height = 0.58f, speed = 0;

    private bool move;

    public static float Z { get => z; set => z = value; }

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
            Ball.z += speed * 0.025f;
        }

        transform.position = new Vector3 (0, height, Ball.z);

    }



}
