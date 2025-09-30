using UnityEngine;
using UnityEngine.UIElements;

public class OrbitingMoon : MonoBehaviour
{

    public GameObject orbitTarget;

  [SerializeField]  private float orbitSpeed;
   [SerializeField] private float orbitRadius;

    private float degreeAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        degreeAngle = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        OrbitTarget(orbitRadius, orbitSpeed, orbitTarget);
    }

    private void OrbitTarget(float radius, float speed, GameObject target)
    {
        degreeAngle += speed * Time.deltaTime;

        if (degreeAngle > 360) 
        {
            degreeAngle = 0;
        }
        float radAngle = degreeAngle * Mathf.Deg2Rad;

        transform.position = target.transform.position + new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0) * radius;

       
    }
}
