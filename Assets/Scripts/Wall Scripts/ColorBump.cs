using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBump : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private Color color;

    public Color Color { get => color; set => color = value; }

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }


    void Start()
    {

        transform.parent = null;
        transform.rotation = Quaternion.EulerRotation(Vector3.zero);

        color = GameController.Instance.colors[Random.Range(0,GameController.Instance.colors.Length)];
        meshRenderer.material.color = color;
    }


    

}
