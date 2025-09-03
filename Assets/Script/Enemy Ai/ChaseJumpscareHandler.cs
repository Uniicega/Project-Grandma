using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChaseJumpscareHandler : MonoBehaviour
{
    [SerializeField] GameObject playerCam;
    [SerializeField] float MoveSpeed = 10;
    float MinDist = 3;

    [SerializeField] Animator ghostAnimator;
    [SerializeField] Animator UiAnimator;
    bool isChasing = false;

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onFinishAnimationEvent += FinishAnimationEvent;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onFinishAnimationEvent -= FinishAnimationEvent;
    }

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player");
        GameObject uiGameObject = GameObject.FindGameObjectWithTag("Jumpscare");
        uiGameObject.GetComponent<Image>().enabled = true;
        UiAnimator = uiGameObject.GetComponent<Animator>();

    }

    void Update()
    {
        Vector3 camPosition = playerCam.transform.position;
        camPosition.y = transform.position.y;
        transform.LookAt(camPosition);

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    public void StartJumpscare()
    {
        GameEventsManager.instance.anomalyEvents.TriggerAttackAnomaly();
        Invoke("BeginTurning", 2);
    }

    private void BeginTurning()
    {
        ghostAnimator.SetTrigger("Turning");
    }

    private void FinishAnimationEvent(string name)
    {
        if(name == "FinishTurning")
        {
            ghostAnimator.SetTrigger("Running");
            isChasing = true;
        }
        else if(name == "FinishJumpscare")
        {
            GameManager.instance.levelManager.FinishedDefeatAnim();
            /*UiAnimator.gameObject.SetActive(false);
            UiAnimator.SetTrigger("ExitJumpscare");
            Destroy(this.gameObject);*/
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, playerCam.transform.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else
        {
            if(isChasing)
            {
                isChasing = false;
                UiAnimator.gameObject.SetActive(true);
                UiAnimator.SetTrigger("TriggerJumpscare");
            }
            
        }
    }
}