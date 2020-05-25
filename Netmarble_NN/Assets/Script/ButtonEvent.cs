using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool bPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        bPressed = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        bPressed = true;
    }
}
