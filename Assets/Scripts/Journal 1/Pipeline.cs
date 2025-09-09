using Unity.VisualScripting;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    Vector2 currentPos;
    Vector2 lastPos;
    Vector2 mousePos;

    float totalMag;

    bool firstDraw;

    float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstDraw = true;
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (firstDraw == true)
            {
                //Debug.Log("Started Drawing");
                firstDraw = false;
                t = Time.deltaTime;
                currentPos = mousePos;
            }

            if (t - Time.deltaTime > 0.1)
            {
                
                lastPos = currentPos;
                currentPos = mousePos;
                Vector2 minusPos = currentPos - lastPos;
    

                Debug.DrawLine(lastPos, currentPos, Color.red);
                totalMag += Mathf.Sqrt(minusPos.x * minusPos.x + minusPos.y * minusPos.y);
           

                t = Time.deltaTime;

            }
            t += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    private void StopDrawing()
    {
        Debug.Log(totalMag);
 
        totalMag = 0;
        firstDraw = true;
    }
}
