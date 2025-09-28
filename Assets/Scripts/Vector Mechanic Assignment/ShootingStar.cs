using UnityEngine;
using System.Collections;

public class ShootingStar : MonoBehaviour
{

    public GameObject shipBat;
    public Bat batScript;

    private bool wasHit;
    //DIRECTION VARIABLES
    Vector2 startDirection;
    float startDirectionX;
    float startDirectionY;
    Vector2 flightDirection;


    //SPEED VARIABLES
     [SerializeField] float speed;
    private Vector2 velo;

    //POSITION VARIABLES
    Vector2 startPos;

    //RADIUS VARIABLE
    float radius;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //For testing, make it start somewhere near top of screen
        startPos.x = 0;
        startPos.y = 5;
        transform.position = startPos;

        //For testing, will always shoot some downwards direction
        startDirectionX = Random.Range(-0.15f, 0.15f);
        startDirectionY = Random.Range(-0.2f, 0f);

        startDirection = new Vector2(startDirectionX, startDirectionY);
        flightDirection = startDirection.normalized;

        batScript = shipBat.GetComponent<Bat>();

        radius = transform.localScale.x / 2;
        
        wasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        velo = flightDirection * speed * Time.deltaTime;

        transform.position += (Vector3) velo;

        CheckForBat();

    }

    private void CheckForBat()
    {
       
        for (int i = 0; i < batScript.contactPoints.Length; i++)
        {
            if (((Vector3) batScript.contactPoints[i] - transform.position).magnitude < radius && !wasHit)
            {
                //For testing, invert the flight direction on contact
                wasHit = true;
                flightDirection *= -1;
            }
        }
    }
}
