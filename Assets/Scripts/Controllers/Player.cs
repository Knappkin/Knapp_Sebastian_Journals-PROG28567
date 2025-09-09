using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(new Vector3(0, 1));
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
}
