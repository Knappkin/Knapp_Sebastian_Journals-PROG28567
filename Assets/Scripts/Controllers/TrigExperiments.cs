using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TrigExperiments : MonoBehaviour
{

    List<float> listOfAnglesInDeg = new List<float>();

    [SerializeField] private float radius;
    [SerializeField] private Vector3 circleEndPos;

    private int angleIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float amountToAdd = 360 / 10;
        for (int i = 0; i < 10; i++)
        {
            listOfAnglesInDeg.Add(i * amountToAdd);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            angleIndex += 1;
        }

        if (angleIndex == listOfAnglesInDeg.Count)
        {
            angleIndex = 0;
        }

        float angleInDegree = listOfAnglesInDeg[angleIndex];
        float angleInRadians = angleInDegree * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        Debug.DrawLine(circleEndPos, new Vector3(x, y, 0f)*radius + circleEndPos, Color.red);
    }

   
    }
