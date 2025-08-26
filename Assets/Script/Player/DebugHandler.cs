using UnityEngine;

public class DebugHandler : MonoBehaviour
{
    void Update()
    {
        HandleDebugToggles();
    }

    private void HandleDebugToggles()
    {
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            GameEventsManager.instance.debugEvents.PressHighlight();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameEventsManager.instance.debugEvents.ActivateLightAnomalies();
            GameEventsManager.instance.anomalyEvents.TriggerLightAnomaly();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEventsManager.instance.debugEvents.ActivateHeavyAnomalies();
            GameEventsManager.instance.anomalyEvents.TriggerHeavyAnomaly();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameEventsManager.instance.debugEvents.ActivateAttackAnomalies();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameEventsManager.instance.debugEvents.RefillIncense();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameEventsManager.instance.debugEvents.SnapIncense();
        }*/
    }
}
