using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ShapeDestroyedHandler(int pointValue);
public class ScoreManager : MonoBehaviour
{
    public  event ShapeDestroyedHandler ScoreUpdated;
    public int collectableShape;

    void Update()
    {
        CalculateScore();
    }

    void CalculateScore()
    {
        ScoreUpdated(collectableShape);
    }

}
