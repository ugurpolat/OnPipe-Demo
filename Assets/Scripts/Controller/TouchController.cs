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
