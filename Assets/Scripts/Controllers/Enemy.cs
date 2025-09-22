using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public GameObject playerShip;
    private float baseSpeed;
    private float maxSpeed;

    private float accelTarget;
    [SerializeField] private float accelMin;
    [SerializeField] private float accelMax;

    [SerializeField] private float accelTime;

    [SerializeField] private float minBaseSpeed;
    [SerializeField] private float maxBaseSpeed;

    private Vector2 enemyVelo;

    private float acceleration;

    private Vector2 targetDirection;

    private Vector2 screenPos;

    private void Start()
    {
        SpawnOnEdge();
    }
    private void Update()
    {

        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        MoveEnemy();
        
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            SpawnOnEdge();
        }
    }

    private void SpawnOnEdge()
    {
        int spawnLocation = Random.Range(0, 4);
        //int spawnLocation = 2;

        float screenWidth = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0)).x;
        float screenHeight = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;

        float horizontalPos = Random.Range(-screenWidth, screenWidth);
        float verticalPos = Random.Range(-screenHeight, screenHeight);
        
        //TOP EDGE
        if (spawnLocation == 0)
        {
            Debug.Log("TOP");
            transform.position = new Vector2(horizontalPos, screenHeight);
        }
        //RIGHT EDGE
        if (spawnLocation == 1)
        {
            Debug.Log("RIGHT");
            transform.position = new Vector2(screenWidth, verticalPos);
        }
        //BOTTOM EDGE
        if (spawnLocation == 2)
        {
            Debug.Log("BOTTOM");
            transform.position = new Vector2(horizontalPos, -screenHeight);
        }
        //LEFT EDGE
        if (spawnLocation == 3)
        {
            Debug.Log("LEFT");
            transform.position = new Vector2(-screenWidth, verticalPos);
        }


        //GET DIRECTION TO MOVE
        targetDirection = (playerShip.transform.position - transform.position).normalized;

        //SET ACCEL GOAL AND BASE SPEED
        accelTarget = Random.Range(accelMin, accelMax);

        acceleration = accelTarget / accelTime;

        float baseSpeed = Random.Range(minBaseSpeed, maxBaseSpeed);

        enemyVelo = targetDirection * baseSpeed;
    }

    private void MoveEnemy()
    {
        enemyVelo += targetDirection * acceleration * Time.deltaTime;

        transform.position += (Vector3) enemyVelo * Time.deltaTime;
    }

}