using UnityEngine;

public class PointClickCamera : MonoBehaviour
{
    [Header("Camera")]
    public GameObject PointCam;
    public float camSmoothTime;
    public float camSpeed;
    public float turningOffset;

    float currentRotationY = 0;
    float TargetRotationY = 0;
    bool isTurning = false;

    public void TurnCameraLeft()
    {
        if (!isTurning)
        {
            TargetRotationY -= 90;
            isTurning = true;
        }       
    }

    public void TurnCameraRight()
    {
        if (!isTurning)
        {
            TargetRotationY += 90;
            isTurning = true;
        }
    }

    private void Update()
    {
        currentRotationY = Mathf.SmoothDamp(currentRotationY, TargetRotationY, ref camSpeed, camSmoothTime);
        PointCam.transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
        if(currentRotationY >= TargetRotationY - turningOffset && currentRotationY <= TargetRotationY + turningOffset)
        {
            isTurning = false;
        }
    }
}