using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public GameObject powerUpPrefab;

    public Vector3 shipVelo;
    public float shipSpeed;
    public float warpValue;
    public int numberOfBombs;
    public float bombSpacing;

    public float cornerDistance;

    public float laserRange;
    public float maxSpeed;
    public float accelTime;
    private float acceleration;
    [SerializeField] Vector2 direction;
    [SerializeField] private float decelTime;
    private float decel;
    private Vector3 decelDirection;

    //Boolean to start timer for testing how long acceleration takes to reach max speed
    private float accelTestTimer;
    private bool testingAccel;

    private float decelTestTimer;
    private bool testingDecel;

    //Bomb offset was used for in class exercise, but journal task uses bombSpacing instead, which is why there will be bits of both in the code. Bomb offset usage will be replaced
    public Vector3 bombOffset;


    //RADAR VARIABLES
    [SerializeField] private float radarRadius;
    [SerializeField] private int radarPointCount;
   

    public void Start()
    {
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Input.GetKeyDown(KeyCode.W) && warpValue <= 1)
        {
            WarpDrive(warpValue);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombSpacing, numberOfBombs);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnBombOnRandomCorner(cornerDistance);
        }

        DetectAsteroids(laserRange, asteroidTransforms);

        PlayerMovement();

        DrawRadar(radarRadius, radarPointCount);
    }

   public void SpawnBombAtOffset(Vector3 inOffset)
    {
        StartCoroutine(PlaceBomb(inOffset));
    }

    public IEnumerator PlaceBomb(Vector3 inOffset)
    {
        float t = Time.deltaTime;
        while(t < 3)
        {
            t += Time.deltaTime;
            yield return null;
        }
        GameObject bombPlaced = Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
        Destroy(bombPlaced, 3);
    }

    public void WarpDrive(float warpDist)
    {
        float warpDistance = Mathf.Lerp(0, (enemyTransform.position - transform.position).magnitude, warpDist);
   
        Vector3 enemyDirection = (enemyTransform.position - transform.position).normalized;

        Vector3 warpPosition = enemyDirection * warpDistance;

        transform.position += warpPosition;    

    }


    public void SpawnBombTrail(float bombSpacing, int bombCount)
    {
        for (int i = 0; i < bombCount; i++)
        {
            
            SpawnBombAtOffset(new Vector3(0, (i+1)*-bombSpacing,0));
        }
    }

    public void SpawnBombOnRandomCorner(float spawnDist)
    {
        int chosenCorner;

        List<Vector2> corners = new List<Vector2>();

        Vector2 topLeft = new Vector2(transform.position.x - spawnDist, transform.position.y + spawnDist);
        corners.Add(topLeft);

        Vector2 topRight = new Vector2(transform.position.x  + spawnDist, transform.position.y + spawnDist);
        corners.Add(topRight);

        Vector2 botLeft = new Vector2(transform.position.x - spawnDist, transform.position.y -spawnDist);
        corners.Add(botLeft);

        Vector2 botRight = new Vector2(transform.position.x + spawnDist, transform.position.y -spawnDist);
        corners.Add(botRight);

    chosenCorner = Random.Range(0, corners.Count);

        SpawnBombAtOffset(corners[chosenCorner]);

    }

    public void DetectAsteroids(float maxRange, List<Transform> asteroidTforms)
    {
        for (int i = 0; i < asteroidTforms.Count; i++)
        {
            float distToAsteroid = (asteroidTforms[i].position - transform.position).magnitude;
            
            if (distToAsteroid < maxRange)
            {
               
               //Debug.Log(distToAsteroid);
                
                Vector2 directionToAsteroid = (asteroidTforms[i].position - transform.position).normalized;
         
                Vector2 laserEndPoint = (Vector2)transform.position + directionToAsteroid * 2.5f;

                //Line I think should work
                Debug.DrawLine(transform.position, laserEndPoint, Color.green);

            }
        }
    }

    public void PlayerMovement()
    {
        acceleration = maxSpeed / accelTime;
        decel = maxSpeed / decelTime;
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            direction.x -= 1;
            
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {    

            direction.x  += 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
        
            direction.y += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            direction.y -= 1;
        }

        direction = direction.normalized;



        // TESTING TIME FOR ACCEL/DECEL
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            accelTestTimer = Time.deltaTime;
            testingAccel = true;
        }

        if (testingAccel)
        {
            accelTestTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            decelTestTimer = Time.deltaTime;
            testingDecel = true;
        }

        if (testingDecel)
        {
            decelTestTimer += Time.deltaTime;
        }



        //COMBINED WAY
        if (direction.magnitude != 0)
        {
            shipVelo += (Vector3)direction * acceleration * Time.deltaTime;
        }

        else
        {
            shipVelo -= shipVelo.normalized * decel * Time.deltaTime;
        }


        //X AND Y WAY

        //if (direction.x != 0)
        //{
        //    shipVelo.x += direction.x * acceleration * Time.deltaTime;
        //}
        //else
        //{
        //    shipVelo.x -= Mathf.Sign(shipVelo.x) * decel * Time.deltaTime;

        //}

        //if (direction.y != 0)
        //{
        //    shipVelo.y += direction.y * acceleration * Time.deltaTime;
        //}

        //else
        //{
        //    shipVelo.y -= Mathf.Sign(shipVelo.y) * decel * Time.deltaTime;
        //}

        //if (shipVelo.x > -0.01 && shipVelo.x < 0.01)
        //{
        //    shipVelo.x = 0;
        //}

        //if (shipVelo.y > -0.01 && shipVelo.y < 0.01)
        //{
        //    shipVelo.y = 0;
        //}

        //shipVelo.x = Mathf.Clamp(shipVelo.x, -maxSpeed, maxSpeed);
        //shipVelo.y = Mathf.Clamp(shipVelo.y, -maxSpeed, maxSpeed);



        shipVelo = Vector3.ClampMagnitude(shipVelo, maxSpeed);

        //TESTING FUNCTIONS
        if(testingAccel && shipVelo.magnitude == maxSpeed)
        {
            Debug.Log(accelTestTimer);
            testingAccel = false;
        }

        if (testingDecel && shipVelo.magnitude < 0.01)
        {
            Debug.Log(decelTestTimer);
            testingDecel = false;
        }

        //ACTUALLY MOVING THE PLAYER
        transform.position += shipVelo * Time.deltaTime;
    }

    private void DrawRadar(float radarRad, int numOfPoints)
    {

        Color radarColour;
        float angleIncrement = 360 / numOfPoints;

        float[] radarAngles = new float[numOfPoints];

        //SETTING COLOUR OF RADAR
        if ((transform.position - enemyTransform.position).magnitude < radarRad)
        {
            radarColour = Color.red;
        }

        else
        {
            radarColour = Color.green;
        }

        for (int i = 0; i < numOfPoints; i++)
        {
            float angleInRad = angleIncrement * i * Mathf.Deg2Rad;

            radarAngles[i] = angleInRad;
        }

        for (int i = 0; i < numOfPoints; i++)
        {
            Vector2 currentPoint = transform.position + new Vector3(Mathf.Cos(radarAngles[i]), Mathf.Sin(radarAngles[i]), 0f) * radarRad;

            Vector2 nextPoint;

            if (i == numOfPoints - 1)
            {
                nextPoint = transform.position + new Vector3(Mathf.Cos(radarAngles[0]), Mathf.Sin(radarAngles[0]), 0f) * radarRad;
            }
            else {
                nextPoint = transform.position + new Vector3(Mathf.Cos(radarAngles[i + 1]), Mathf.Sin(radarAngles[i + 1]), 0f) * radarRad;
            }

            Debug.DrawLine(currentPoint, nextPoint, radarColour);
        }
    }

    private void SpawnPowerup()
    {

    }
}
