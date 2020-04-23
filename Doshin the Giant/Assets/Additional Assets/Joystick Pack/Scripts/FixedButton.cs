using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // IPointer events help extend the Button library to check for being pressed
    [HideInInspector]
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**** If pointer is read as down, is pressed is true ****/
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    /**** If pointer is read as up, is pressed is false ****/
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
