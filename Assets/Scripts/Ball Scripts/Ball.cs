 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float z;

    private static Color currentColor;

    private MeshRenderer meshRenderer;


    private float height = 0.58f, speed = 300;

    private bool move;

    public static float Z { get => z; set => z = value; }
    public static Color CurrentColor { get => currentColor; set => currentColor = value; }

    private void Awake()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();



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
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Hit"))
        {
            print("We hit the wall !!!");
        }else if (other.CompareTag("Fail"))
        {
            print("We hit fail");
        }
        else if (other.CompareTag("FinishLine"))
        {
            print("Finish");

        }

    }


}
