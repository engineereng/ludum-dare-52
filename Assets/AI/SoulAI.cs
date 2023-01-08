using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI : MonoBehaviour
{
    [SerializeField]
    private List<Detector> detectors;
    [SerializeField]
    private AIData aiData;
    [SerializeField]
    private float detectionDelay = 0.05f;

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        Debug.Log("performing detection");
        foreach(Detector detector in detectors)
        {
            Debug.Log("performing detection");
            detector.Detect(aiData);
        }
    }
}
