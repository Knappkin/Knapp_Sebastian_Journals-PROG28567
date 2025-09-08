using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class RowGenerator : MonoBehaviour
{

    public TMPro.TMP_InputField numberField;
    private List<Vector2> squaresList = new List<Vector2>();
    private float squareSize = 2f;
    private float paddingSize =  0.5f;
    int rowSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rowSize != 0)
        {
            DrawSquares();
        }
    }

    public void SpawnRow()
    {
        string textEntered;
 
       
        
        if (numberField != null) {

            Debug.Log("Got here");
            textEntered = numberField.text;
            

            if (int.TryParse(textEntered, out rowSize) && rowSize > 0){
               
                Debug.Log(rowSize);
                
            }
           
            
        }

    }

    private void DrawSquares()
    {
        Debug.Log("Now we here");
        float startSpot;

        //Find spawn location of first square
        startSpot = 0 - rowSize / 2 * squareSize - rowSize / 2 * paddingSize;
        
        for(int i = 0; i < rowSize; i++)
        {
            //find center of square
            Vector2 squarePosition = new Vector2(startSpot + i* squareSize + i * paddingSize,0);
            Debug.Log(squarePosition);
            Vector2 topLeft;
            Vector2 topRight;
            Vector2 bottomRight;
            Vector2 bottomLeft;

            //Top Left
            topLeft = new Vector2(squarePosition.x - squareSize / 2, squarePosition.y + squareSize / 2);
            //Top Right
            topRight = new Vector2(squarePosition.x + squareSize / 2, squarePosition.y + squareSize/2);
            //Bottom Left
            bottomLeft = new Vector2(squarePosition.x - squareSize / 2, squarePosition.y - squareSize / 2);
            //Bottom Right
            bottomRight = new Vector2(squarePosition.x + squareSize / 2, squarePosition.y - squareSize / 2);

            Debug.DrawLine(topLeft, topRight);
            Debug.DrawLine(topRight, bottomRight);
            Debug.DrawLine(bottomLeft, bottomRight);
            Debug.DrawLine(topLeft, bottomLeft);
            
        }
    }
    
}
