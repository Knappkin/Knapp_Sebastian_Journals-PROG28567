using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    private float shipSpeed = 2f;
    public float warpValue;
    public int numberOfBombs;
    public float bombSpacing;

    public float cornerDistance;

    public float laserRange;

    //Bomb offset was used for in class exercise, but journal task uses bombSpacing instead, which is why there will be bits of both in the code. Bomb offset usage will be replaced
    public Vector3 bombOffset;

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

                //Line to the direction without multiplying it
                //Debug.DrawLine(transform.position, directionToAsteroid, Color.red);

                //Line to the actual positions of the asteroids
                //Debug.DrawLine(transform.position, asteroidTforms[i].position, Color.blue);
            }
        }
    }
}
