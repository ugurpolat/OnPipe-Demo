using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cornRound;

    [SerializeField]
    private GameObject endCircle;

    [SerializeField]
    private GameObject obstacle;

    float height;
    float offSet;
    float minRange;
    float maxRange;
    float distanceRange;
    int calculateCornAll;
    Vector3 pos;
    Vector3 obstacleSize = new Vector3(2.2f, 2.5f, 2.2f);

    private void Start()
    {
        SpawnPosition();
    }

    /// <summary>
    /// This function works to create the collectible pieces that form
    /// on the rollers during the game and the finish circle that should
    /// be formed at the end of the level.
    /// </summary>
    private void SpawnPosition()
    {
        SetGemSize();

        if (gameObject.tag == "Cylinder")
        {
            CreateGem();
        }
        if (gameObject.tag == "EndCylinder")
        {
            endCircle = Instantiate(endCircle, new Vector3(transform.position.x, transform.position.y, maxRange - 25f), endCircle.transform.rotation);
            endCircle.tag = "EndCircle";
            endCircle.transform.parent = gameObject.transform;
        }

    }

    /// <summary>
    /// This function calculates where the collectible pieces
    /// to be created in cylinders of different sizes begin and end. 
    /// When calculating this, it finds how many pieces will fit.
    /// </summary>
    void SetGemSize()
    {
        height = transform.localScale.y;
        offSet = height / 4;

        minRange = transform.position.z - transform.localScale.y + offSet;
        maxRange = transform.position.z + transform.localScale.y - offSet;

        distanceRange = maxRange - minRange;
        calculateCornAll = (int)(distanceRange / 0.2f);

        pos = new Vector3(transform.position.x, transform.position.y, minRange);

    }

    /// <summary>
    /// Created gems for cylinder size and created obstable which is not connected cylinder
    /// </summary>
    void CreateGem()
    {
        for (int i = 0; i <= calculateCornAll; i++)
        {
            cornRound = Instantiate(cornRound, pos, cornRound.transform.rotation);

            if (transform.localScale.y > 5.5f)
            {
                cornRound.transform.localScale = new Vector3(transform.localScale.x * (0.5f),
                                                         transform.localScale.y * 0.2f,
                                                         transform.localScale.z * (0.56f));
            }
            else if (transform.localScale.y < 5.5f)
            {
                cornRound.transform.localScale = new Vector3(transform.localScale.x * (0.5f),
                                                         transform.localScale.y * 0.25f,
                                                         transform.localScale.z * (0.5f));
            }

            cornRound.transform.parent = gameObject.transform;
            pos.z += 0.17f;

            if (pos.z > maxRange)
            {
                break;
            }

        }
        if (Random.value < 0.1f)
        {
            CreatedAirObstacle();
        }
    }

    /// <summary>
    /// non connected obstacle
    /// </summary>
    void CreatedAirObstacle()
    {
        obstacle.transform.localScale = obstacleSize;
        obstacle = Instantiate(obstacle, new Vector3(pos.x, pos.y, Random.Range(minRange - offSet, maxRange + offSet)), cornRound.transform.rotation);
        obstacle.GetComponent<Renderer>().material.color = Color.red;
        obstacle.transform.parent = gameObject.transform;
        obstacle.gameObject.tag = "Obstacle2";
    }
}
