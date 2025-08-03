using UnityEngine;

public class PointClickCamera : MonoBehaviour
{
    [Header("Camera Movement Setting")]
    public GameObject PointCam;
    public float camSmoothTime;
    public float camSpeed;
    public float turningOffset;

    [Header("Cameras")]
    private GameObject currentCam;
    [SerializeField] GameObject windowCam;
    [SerializeField] GameObject coffinCam;
    [SerializeField] GameObject doorCam;
    [SerializeField] GameObject curtainCam;
    [SerializeField] GameObject ceilingCam;
    private int cameraIndex;

    [SerializeField] Animator sceneTransition;

    float currentRotationY;
    float targetRotationY = 0;
    bool isTurning = false;


    private void Start()
    {
        currentCam = windowCam; //Set camera to default position
        cameraIndex = 1;
        SetCamPosition();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //When getting key input, set target for camera to turn to
        {
            TurnCameraLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnCameraRight();
        }

        if(isTurning)
        {
            HandleCameraMovement(); //Do camera movement stuff
        }
        
    }

    public void TurnCameraLeft()
    {
        if (!isTurning) //Don't turn if the player is already turning
        {
            sceneTransition.SetTrigger("TransitionOut"); //Play fade to black animation
            targetRotationY -= 90; //Set camera turn target to 90 degree to the left
            isTurning = true; //Keep track of when player is turning

            cameraIndex--;//Switch target camera index to the one on its left
            if(cameraIndex < 1)
            {
                cameraIndex = 4; //loop value back incase of underflow
            }
        }

    }

    public void TurnCameraRight()
    {
        if (!isTurning) //Don't turn if the player is already turning
        {
            sceneTransition.SetTrigger("TransitionOut"); //Play fade to black animation
            targetRotationY += 90; //Set camera turn target to 90 degree to the right
            isTurning = true; //Keep track of when player is turning

            cameraIndex++; //Switch target camera index to the one on its right
            if (cameraIndex > 4)
            {
                cameraIndex = 1; //loop value back incase of overflow
            }
        }
    }

    private void HandleCameraMovement()
    {
        currentRotationY = Mathf.SmoothDamp(currentRotationY, targetRotationY, ref camSpeed, camSmoothTime); //Smooth camera turn (janky camera turn target rn FIX LATER)
        PointCam.transform.rotation = Quaternion.Euler(currentCam.transform.eulerAngles.x, currentRotationY, currentCam.transform.eulerAngles.z);

        if (currentRotationY >= targetRotationY - 5*turningOffset && currentRotationY <= targetRotationY + 5*turningOffset && isTurning) //Teleport camera to target position when it has almost turned toward the target
        {
            SwitchCamera();
            sceneTransition.SetTrigger("TransitionIn");
        }

        if (currentRotationY >= targetRotationY - turningOffset && currentRotationY <= targetRotationY + turningOffset && isTurning) //Enable turning again once player has turned toward the target
        {
            isTurning = false;
        }
    }

    private void SwitchCamera() //Set target camera based on the index
    {
        if(cameraIndex == 1)
        {
            currentCam = windowCam;
        }
        else if(cameraIndex == 2)
        {
            currentCam = coffinCam;
        }
        else if (cameraIndex == 3)
        {
            currentCam = doorCam;
        }
        else if (cameraIndex == 4)
        {
            currentCam = curtainCam;
        }
        SetCamPosition();
    }

    private void SetCamPosition() //Set the camera position and rotation to the target camera
    {
        PointCam.transform.position = currentCam.transform.position;
        PointCam.transform.rotation = currentCam.transform.rotation;
        currentRotationY = currentCam.transform.eulerAngles.y ;
        targetRotationY = currentCam.transform.eulerAngles.y;
    }
}