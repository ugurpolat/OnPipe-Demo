using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void TouchEventHandler(bool touch);

public class TouchController : MonoBehaviour
{
    public static event TouchEventHandler TapEvent;
    public static event TouchEventHandler TapEndEvent;

    bool didTouch;
    
    void OnTap()
    {
        if (TapEvent != null)
        {
            TapEvent(didTouch);
        }
    }

    void OnTapEnd()
    {
        if (TapEvent != null)
        {
            TapEndEvent(didTouch);
        }
    }
    /// <summary>
    /// In the update function it controls touch movement of the user 
    /// touch screen OnTap function will work and tap event function send a message to subscribers
    /// If the user takes their finger off the screen tapend event function send a message to subscribers
    /// </summary>
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            
            if (touch.phase == TouchPhase.Began)
            {               
                OnTap();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                OnTapEnd();
            }
        }
    }
}
