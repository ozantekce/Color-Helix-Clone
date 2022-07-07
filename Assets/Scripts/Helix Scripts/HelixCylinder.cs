using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixCylinder : MonoBehaviour
{

    private GameObject helix;



    // Start is called before the first frame update
    void Awake()
    {
        helix = GameObject.Find("Helix");    
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.eulerAngles = new Vector3(0,0,helix.transform.eulerAngles.z%25 );


    }


}
