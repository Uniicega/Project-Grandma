using System.Collections;
using UnityEngine;

public class FlashlightOverlay : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Animator animator;
    [SerializeField] Animation lightFlickering;
    [SerializeField] Animation heavyFlickering;

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onTriggerLightAnomaly += TriggerLightAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerHeavyAnomaly += TriggerHeavyAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerAttackAnomaly += TriggerAttackAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerChasedAnomaly += TriggerChasedAnomaly;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onTriggerLightAnomaly -= TriggerLightAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerHeavyAnomaly -= TriggerHeavyAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerAttackAnomaly -= TriggerAttackAnomaly;
        GameEventsManager.instance.anomalyEvents.onTriggerChasedAnomaly -= TriggerChasedAnomaly;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, mousePosition, canvas.worldCamera, out uiPosition); //Position magic to get canvas position of the mouse
        transform.position = canvas.transform.TransformPoint(uiPosition); //Teleport slider to the mouse position
    }

    private void TriggerLightAnomaly()
    {
        animator.SetTrigger("LightFlickering");
        StartCoroutine(WaitForFlickering());
    }

    IEnumerator WaitForFlickering()
    {
        yield return new WaitForSeconds(0.13f);
        animator.SetTrigger("Default");
    }

    private void TriggerHeavyAnomaly()
    {
        animator.SetTrigger("HeavyFlickering");
        StartCoroutine(WaitForFlickering());
    }

    private void TriggerAttackAnomaly()
    {
        animator.SetTrigger("LightDown");
    }

    private void TriggerChasedAnomaly() 
    {
        animator.SetTrigger("Default");
    }
}
