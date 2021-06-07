using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : MonoBehaviour
{
    [SerializeField]
    Text fpsText;

    /// <summary>
    /// If you want to see your fps on your phone
    /// Open canvas panel -> Make FPSText enable
    /// </summary>
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = fps.ToString();
    }
}
