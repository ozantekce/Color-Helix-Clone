using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{

    private MeshRenderer meshRenderer;



    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Start()
    {
        if(this.gameObject.tag == "Hit")
        {

            GameObject colorBump = GameObject.FindGameObjectWithTag("ColorBump");
            
            if(colorBump!=null && transform.parent.parent.transform.position.z > colorBump.transform.position.z)
            {
                //Debug.Log(transform.parent.transform.position.z + " "+colorBump.transform.position.z);
                GameController.Instance.hitColor = colorBump.GetComponent<ColorBump>().Color;
                
            }


            meshRenderer.material.color = GameController.Instance.hitColor;
        }
        else
        {

            if(GameController.Instance.failColor == GameController.Instance.hitColor)
            {
                GameController.Instance.failColor 
                    = GameController.Instance.colors[Random.Range(0,GameController.Instance.colors.Length)];
            }
            meshRenderer.material.color= GameController.Instance.failColor;

        }

        transform.localPosition = Vector3.zero;
        


    }


}
