using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenarator : MonoBehaviour
{
    public GameObject currentScorePanel;
    public GameObject winPanel;
    UIManager uıManager;
    [Range(1f, 2.0f)]
    public float minRadius;

    [Range(2f, 4f)]
    public float maxRadius;

    [Range(2f, 4f)]
    public float minHeight;

    [Range(4f, 7f)]
    public float maxHeight;

    [SerializeField]
    private GameObject cylinder;

    [SerializeField]
    private GameObject obstacle;

    GameObject secondCylinder;
    GameObject endCylinder;

    float spawnPosition;
    float radius;
    float height;
    float radiusforObstacle;
    float heightForObstacle;
    float spawnObstaclePosition;

    private void Awake()
    {
        uıManager = GameObject.FindObjectOfType<UIManager>();
        PlayerPrefs.GetInt("Level", 1);
    }

    private void Start()
    {
        CreateLevel();
    }

    // there are two float value for cylinder radius when created cylinder in the map
    private float SetRadius(float min_Radius, float max_Radius)
    {
        float radius = Random.Range(min_Radius, max_Radius);

        if (secondCylinder != null)
        {
            while (Mathf.Abs(radius - secondCylinder.transform.localScale.x) < 0.5f)
            {
                radius = Random.Range(min_Radius, max_Radius);
            }
        }

        return radius;
    }

    // preparing cylinder before set position
    void SetCylinderSettings()
    {
        radius = SetRadius(minRadius, maxRadius);
        height = Random.Range(minHeight, maxHeight);

        radiusforObstacle = Random.Range(1f, 4f);
        heightForObstacle = Random.Range(0.5f, 1f);

        cylinder.transform.localScale = new Vector3(radius, height, radius);
    }

    //creating red cylinder for obstacle
    void CreateObstacleCylinder()
    {
        cylinder.transform.localScale = new Vector3(radiusforObstacle, heightForObstacle, radiusforObstacle + 0.5f);
        spawnObstaclePosition = secondCylinder.transform.position.z + secondCylinder.transform.localScale.y + cylinder.transform.localScale.y;
        secondCylinder = Instantiate(cylinder, new Vector3(0, 0, spawnObstaclePosition), cylinder.transform.rotation);
        secondCylinder.tag = "Obstacle";
        secondCylinder.GetComponent<Renderer>().material.color = Color.red;
    }

    //creating white cylinder
    void CreateCylinder()
    {
        spawnPosition = secondCylinder.transform.position.z + secondCylinder.transform.localScale.y + cylinder.transform.localScale.y;
        secondCylinder = Instantiate(cylinder, new Vector3(0, 0, spawnPosition), cylinder.transform.rotation);
        secondCylinder.tag = "Cylinder";
    }

    //cretaing end cylinder for finish circle
    void CreateEndCylinder()
    {
        endCylinder.transform.localScale = new Vector3(secondCylinder.transform.localScale.x, 28, secondCylinder.transform.localScale.z);
        spawnPosition = secondCylinder.transform.position.z + secondCylinder.transform.localScale.y + endCylinder.transform.localScale.y;
        endCylinder.transform.position = new Vector3(0, 0, spawnPosition);
        endCylinder.tag = "EndCylinder";
    }

    // calculate size obstacle for creating red obstacle
    void SetObstacleCylinder()
    {
        if (secondCylinder.tag != "Obstacle")
        {
            if (secondCylinder.transform.localScale.z < cylinder.transform.localScale.z)
            {
                CreateObstacleCylinder();
            }
            else
            {
                CreateObstacleCylinder();
            }

        }
    }

    public void CreateLevel()
    {
        BeforeCreatedMap();
        for (int i = 0; i < 10; i++)
        {
            SetCylinderSettings();

            if (i == 0)
            {
                secondCylinder = Instantiate(cylinder, Vector3.zero, cylinder.transform.rotation);
                secondCylinder.tag = "Cylinder";
            }
            else
            {

                if (Random.value < 0.2f)
                {
                    SetObstacleCylinder();
                }
                else
                {
                    CreateCylinder();
                }

            }

            secondCylinder.transform.parent = gameObject.transform;
        }
        CreateEndCylinder();
        endCylinder.transform.parent = gameObject.transform;
    }

    void BeforeCreatedMap()
    {
        Camera.main.GetComponent<CameraFollow>().transform.position = new Vector3(0, 14, -5.3f);
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        currentScorePanel.SetActive(true);
        winPanel.SetActive(false);
        endCylinder = Instantiate(cylinder, new Vector3(-5, 0, 0), cylinder.transform.rotation);
    }

}
