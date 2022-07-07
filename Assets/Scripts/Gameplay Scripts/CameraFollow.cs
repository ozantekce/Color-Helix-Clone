using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float cameraZ;


    void Update()
    {

        cameraZ = Ball.Z - 2.95f;

        transform.position = new Vector3(0, 2.2f, cameraZ);    
    
    }


}
