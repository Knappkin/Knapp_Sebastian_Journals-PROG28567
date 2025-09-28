using UnityEngine;

public class StarSpawner : MonoBehaviour
{

    public GameObject starPrefab;
    public GameObject bat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnStar();
        }
    }

    private void SpawnStar()
    {
        GameObject spawnedStar = Instantiate(starPrefab);
        ShootingStar starScript = spawnedStar.GetComponent<ShootingStar>();
        starScript.shipBat = bat;
    }
}
