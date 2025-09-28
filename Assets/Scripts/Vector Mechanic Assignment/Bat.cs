using UnityEngine;
using System.Collections.Generic;
public class Bat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float halfBatSize;

    
    // Determine how many points to create in the list
   [SerializeField] private int collisionCheckCount;

    //List to store contact check points
    public Vector2[] contactPoints;

    float pointBufferSize;

    void Start()
    {
        contactPoints = new Vector2[collisionCheckCount];

        pointBufferSize = GetComponent<SpriteRenderer>().transform.localScale.x / collisionCheckCount;


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < contactPoints.Length; i++)
            contactPoints[i] = new Vector2(transform.position.x + pointBufferSize*i, transform.position.y);
            
    }
}
