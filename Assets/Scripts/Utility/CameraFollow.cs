using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Options")]
    [Range(7f, 15f)]
    public float cameraSpeed;

    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        //checking for start panel 
        if (!PlayerController.Instance.startPanel.activeSelf)
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
        }

    }
}