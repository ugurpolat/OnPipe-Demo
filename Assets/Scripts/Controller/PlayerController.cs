using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDead;
    public bool canCollect;
    public Vector3 targetCylinderRadius;
    public Transform targetCylinder;
    public bool didTouch;
    public bool passedLevel;
    public GameObject startPanel;

    Vector3 ringLocalScale = new Vector3(1f, 0.8f, 1f);

    float ringScaleFactor = 0.25f;
    float overlapSphereRadius = 0.5f;
    float offSet = 0.8f;

    private void OnEnable()
    {
        TouchController.TapEvent += TapHandler;
        TouchController.TapEndEvent += TapEndHandler;
    }

    private void OnDisable()
    {
        TouchController.TapEvent -= TapHandler;
        TouchController.TapEndEvent -= TapEndHandler;
    }

    void Update()
    {
        if (!passedLevel)
        {

            targetCylinder = Physics.OverlapSphere(transform.position, overlapSphereRadius, LayerMask.GetMask("Cylinder"))[0].transform;
            targetCylinderRadius = Vector3.zero;

            if (targetCylinderRadius.x + offSet > transform.localScale.x)
            {
                canCollect = true;
            }
            else
            {
                canCollect = false;
            }

            ChangeRingSize();
        }


    }

    void ChangeRingSize()
    {
        //for mouse control

        /*if (Input.GetMouseButton(0))
        {
            targetCylinderRadius = new Vector3(targetCylinder.localScale.x * ringScaleFactor, ringLocalScale.y, targetCylinder.localScale.z * ringScaleFactor);
            transform.localScale = Vector3.Slerp(transform.localScale, targetCylinderRadius, 0.125f);
        }
        else
        {
            if (transform.localScale.x < ringLocalScale.x)
            {
                transform.localScale = Vector3.Slerp(transform.localScale, ringLocalScale, 0.125f);
            }
            else
            {
                transform.localScale = ringLocalScale;
            }
        }
        */

        //for mobile ınput
        if (didTouch)
        {
            targetCylinderRadius = new Vector3(targetCylinder.localScale.x * ringScaleFactor, ringLocalScale.y, targetCylinder.localScale.z * ringScaleFactor);
            transform.localScale = Vector3.Slerp(transform.localScale, targetCylinderRadius, 0.85f);

        }
        else
        {
            if (transform.localScale.x < ringLocalScale.x)
            {
                transform.localScale = Vector3.Slerp(transform.localScale, ringLocalScale, 0.75f);
            }
            else
            {
                transform.localScale = ringLocalScale;
            }
        }
    }

    void TapHandler(bool touch)
    {
        didTouch = true;
        if (startPanel.activeSelf)
        {
            startPanel.SetActive(false);
        }
    }

    void TapEndHandler(bool touch)
    {
        didTouch = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndCircle"))
        {
            passedLevel = true;
        }
        else if (other.CompareTag("Obstacle2"))
        {
            isDead = true;
        }
    }
}
