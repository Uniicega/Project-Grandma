using NUnit.Framework;
using UnityEngine;

public class Incense : MonoBehaviour
{
    [SerializeField] GameObject startObject;
    [SerializeField] GameObject endObject;
    [SerializeField] GameObject incenseStick;
    [SerializeField] Light incenseLight;
    float startingLight;
    private Vector3 initialScale;
    private Vector3 initialDistance;
    private float distance;
    public float incensePercentage;

    private void Start()
    {
        initialScale = incenseStick.transform.localScale;
        initialDistance = (endObject.transform.position - startObject.transform.position);
        endObject.transform.position = startObject.transform.position + (initialDistance * incensePercentage);
        startingLight = incenseLight.range;
    }

    private void Update()
    {
        UpdateTransformForScale();
    }

    private void UpdateTransformForScale()
    {
        if(incensePercentage >= 0)
        {
            endObject.transform.position = startObject.transform.position + (initialDistance * incensePercentage);
        }
        


        distance = Vector3.Distance(startObject.transform.position, endObject.transform.position);
        incenseStick.transform.localScale = new Vector3(initialScale.x, distance / 2, initialScale.z);

        Vector3 middlePoint = (startObject.transform.position + endObject.transform.position) /  2;
        incenseStick.transform.position = middlePoint;

        incenseLight.range = startingLight * incensePercentage;

    }

}
