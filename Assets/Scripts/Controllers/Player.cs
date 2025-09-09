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
    private int numberOfBombs;
    private float bombSpacing;

    //Bomb offset was used for in class exercise, but journal task uses bombSpacing instead, which is why there will be bits of both in the code. Bomb offset usage will be replaced
    public Vector3 bombOffset;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(bombOffset);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WarpDrive(shipSpeed);
        }

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

    public void WarpDrive(float shipSpeed)
    {
        transform.position += (enemyTransform.position - transform.position).normalized * shipSpeed;
    }
}
