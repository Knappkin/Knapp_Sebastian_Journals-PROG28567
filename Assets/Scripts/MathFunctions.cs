using UnityEngine;

public class MathFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Vector2 pos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMagnitude(Vector2 pos)
    {
       return Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y);
    }

    public void DrawSquare(Vector2 pos, float size, Color color, float duration)
    {
        Vector2 topLeft = pos + Vector2.up * size/2 + Vector2.left * size/2;
        Vector2 topRight = pos + Vector2.up * size / 2 + Vector2.right * size / 2;
        Vector2 bottomRight = pos + Vector2.down * size / 2 + Vector2.right * size / 2;
        Vector2 bottomLeft = pos + Vector2.down * size / 2 + Vector2.left * size / 2;

        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topLeft, bottomLeft, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomLeft, bottomRight, color, duration);
    }
}
