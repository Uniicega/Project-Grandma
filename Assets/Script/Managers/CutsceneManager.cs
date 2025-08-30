using Unity.VisualScripting;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private GameObject playerCamera;
    [SerializeField] GameObject ghost;
    [SerializeField] Transform startPos;

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onTriggerAttackAnomaly += PlayAttackCutscene;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onTriggerAttackAnomaly -= PlayAttackCutscene;
    }

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("Player");

    }
    private void Update()
    {
        
    }

    private void PlayAttackCutscene()
    {
        ghost.transform.position = startPos.position;

    }

    private void TurnPlayerCameraToward(Transform lookDir)
    {
        Vector3 direction = lookDir.transform.position - playerCamera.transform.position;
        
    }
}
