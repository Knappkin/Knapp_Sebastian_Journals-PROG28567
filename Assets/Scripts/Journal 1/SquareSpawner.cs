using Unity.VisualScripting;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float squareSize;
    private float scaleSize;

    //Corners
    Vector2 topLeft;
    Vector2 topRight;
    Vector2 bottomRight;
    Vector2 bottomLeft;
    void Start()
    {
        squareSize = 2f;
        scaleSize = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        FindCorners();

        ShowOutline();

        if(Input.GetMouseButtonDown(0))
        {
            DrawSquare();
        }

        //Setting the scale to multiply the square by based on mouse scroll. *0.1 to lower the change per scroll
        scaleSize += Input.mouseScrollDelta.y * 0.1f;
        //Clamping it so it wouldn't go into the negative or get too big
        scaleSize = Mathf.Clamp(scaleSize, 0.2f, 3f);
    }

    private void FindCorners()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        float halfSize = squareSize * scaleSize /2;

        //Top left (-x,+y)
        topLeft = new Vector2(mousePos.x - halfSize, mousePos.y + halfSize);
        Debug.DrawLine(mousePos, topLeft, Color.red);
        //Top right (+x,+y)
        topRight = new Vector2(mousePos.x + halfSize, mousePos.y + halfSize);
        Debug.DrawLine(mousePos, topRight, Color.blue);

        //Bottom right (+x,-y)
        bottomRight = new Vector2(mousePos.x + halfSize, mousePos.y - halfSize);
        Debug.DrawLine(mousePos, bottomRight, Color.yellow);
        //Bottom left (-x, -y)
        bottomLeft = new Vector2(mousePos.x - halfSize, mousePos.y - halfSize);
        Debug.DrawLine(mousePos, bottomLeft, Color.green);
    }

    private void ShowOutline()
    {
        //Top side
        Debug.DrawLine(topLeft, topRight, Color.cyan);

        //Right side
        Debug.DrawLine(topRight, bottomRight, Color.cyan);
        //Bottom side
        Debug.DrawLine(bottomLeft, bottomRight, Color.cyan);
        //Left side
        Debug.DrawLine(topLeft, bottomLeft, Color.cyan);
    }
    private void DrawSquare()
    {
        //Top Side
        Debug.DrawLine(topLeft, topRight, Color.white);
        //Right Side
        Debug.DrawLine(topRight, bottomRight, Color.white);
        //Bottom Side
        Debug.DrawLine(bottomLeft, bottomRight, Color.white);
        //Left Side
        Debug.DrawLine(topLeft, bottomLeft, Color.white);
    }

}