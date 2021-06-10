using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    PlayerController playerController;
    ScoreManager scoreManager;
    public Rigidbody[] corns;

    private void Awake()
    {
        corns = gameObject.GetComponentsInChildren<Rigidbody>();
    }
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

    }
    private void FixedUpdate()
    {
        ControlPlayer();
    }

    public void ControlPlayer()
    {
        bool isTouchedPlayer = Physics.CheckSphere(transform.position, 0.07f, LayerMask.GetMask("Player"));

        if (isTouchedPlayer && playerController.canCollect)
        {
            scoreManager.collectableShape++;
            CornSoloMovment();
            StartCoroutine(DestroyShape());
        }
    }

    //when ring hit corn round after corn solo movment
    void CornSoloMovment()
    {
        for (int i = 0; i < 17; i++)
        {
            if (corns[i].transform.localPosition.z <= 0.31)
            {
                corns[i].AddForce(Vector3.up * Random.Range(1.5f, 2f), ForceMode.Impulse);
                corns[i].useGravity = true;
                corns[i].AddForce(Vector3.back * Random.Range(0.5f, 1f), ForceMode.Impulse);
            }
        }
    }

    IEnumerator DestroyShape()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}