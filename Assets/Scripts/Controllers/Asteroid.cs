using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    private Vector2 nextPosition;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        ChooseNextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAsteroid();
        //Debug.DrawLine(transform.position, nextPosition);
    }

    private void ChooseNextPosition()
    {
        Vector2 randomDirection;
        randomDirection.x = Random.Range(-1, 1);
        randomDirection.y = Random.Range(-1, 1);

        randomDirection = randomDirection.normalized;
        moveDirection = randomDirection;

        nextPosition = transform.position + (Vector3)randomDirection * maxFloatDistance;
        
    }

    private void MoveAsteroid()
    {
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
        if((nextPosition-(Vector2)transform.position).magnitude < arrivalDistance)
        {
            ChooseNextPosition();
        }
    }
}
