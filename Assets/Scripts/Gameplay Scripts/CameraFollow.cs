using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float cameraZ;

    private Animator animator;

    private float time,speed = 2;

    void Awake()
    {

        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    void Update()
    {

        if(time < 1)
        {
            time += Time.deltaTime * speed;
            cameraZ = Mathf.Lerp(transform.position.z, -2.95f, time);
        }
        else
        {
            cameraZ = Ball.Z - 2.95f;
        }

        cameraZ = Ball.Z - 2.95f;

        transform.position = new Vector3(0, 2.2f, cameraZ);    
    
    }


    public void Flash()
    {

        animator.SetTrigger("Flash");
        cameraZ = 0;
        time = 0;
    }


}
