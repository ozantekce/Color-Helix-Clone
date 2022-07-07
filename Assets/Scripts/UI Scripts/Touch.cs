using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private static bool pressing;

    public static bool IsPressing() {
        {/*
            if (pressing)
            {
                print("we are pressing");
            }
            else
            {
                print("we are not pressing");
            }*/

            return pressing;
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {

        pressing = true;


    }

    public void OnPointerUp(PointerEventData eventData)
    {

        pressing=false;


    }



    
}
