using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDisplay : MonoBehaviour
{

    private TextMesh text;


    void Awake()
    {
        text = GetComponent<TextMesh>();
    }


    public void SetText(string text)
    {
        this.text.text = text;
        this.text.color = Color.white;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Ball.Z);

        Destroy(gameObject,1.2f);

    }



}
