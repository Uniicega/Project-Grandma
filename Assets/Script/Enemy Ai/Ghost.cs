using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float MoveSpeed = 10;
    float MinDist = 3;

    [SerializeField] Animator ghostAnimator;
    [SerializeField] Animator UiAnimator;
    bool isChasing = false;

    private void Start()
    {
        //Invoke("StartJumpScare", 6);
    }

    void Update()
    {
        Vector3 camPosition = Player.transform.position;
        camPosition.y = transform.position.y;
        transform.LookAt(camPosition);

        if (isChasing)
        {
            ChasePlayer();
        }
    }


    private void StartJumpScare()
    {
        ghostAnimator.SetTrigger("Turning");
    }

    public void FinishTurningAround()
    {
        ghostAnimator.SetTrigger("Running");
        isChasing = true;
    }

    private void ChasePlayer()
    {
        

        if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else
        {
            isChasing = false ;
            UiAnimator.gameObject.SetActive(true);
            UiAnimator.SetTrigger("TriggerJumpscare");
        }
    }
}