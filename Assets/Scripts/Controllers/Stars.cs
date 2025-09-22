using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private float drawSpeed;

    private float drawDist;

    private Vector2 drawDirection;

    private Vector2 lineEnd;


    private void Start()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        StartCoroutine(DrawPoints());
    }

    private IEnumerator DrawPoints()
    {


        for (int i = 0; i < starTransforms.Count; i++)
        {
            if (i == starTransforms.Count - 1)
            {
                i = 0;
            }
                //Storing position of the current and next stars to access for direction and distance
                Vector2 currentStar = starTransforms[i].position;
                Vector2 nextStar = starTransforms[i + 1].position;

                //Distance between next star and current star
                drawDist = (nextStar - currentStar).magnitude;

                //direction from current star to next
                drawDirection = (nextStar - currentStar).normalized;

                //Setting the line end position - will draw to this point
                lineEnd = currentStar;

                //setting the drawspeed based the draw time variable
                drawSpeed = drawDist / drawingTime;

                while ((nextStar - lineEnd).magnitude > 0.1)
                {
                    lineEnd += drawDirection * drawSpeed * Time.deltaTime;
                    Debug.DrawLine(currentStar, lineEnd);
                    yield return null;
                }

            }

    }
}
