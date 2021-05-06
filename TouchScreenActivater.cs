using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchScreenActivater : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool pressed = false;
    public FPS_Controller controller;
    public UnityEvent OnLongClick;
   

    
  
    public void OnPointerDown(PointerEventData eventData)
    {
        
        pressed = true;
        
       
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
       
    }

    void Update()
    {
        if (pressed == true)
        {
            controller.AttackPress = true;
            
        }
        if (pressed==false)
        {
            controller.AttackPress = false;
        }

        
    }
}
