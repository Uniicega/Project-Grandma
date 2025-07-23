using UnityEngine;

public abstract class Anomaly: MonoBehaviour
{
    //Abstract class for anomalies
    public int currentAnomalyPoint;
    public int anomalyValue;

    private void OnEnable()
    {
        Invoke("SubscribeToEvents", 0.1f);
    }

    private void SubscribeToEvents()
    {
        GameEventsManager.instance.anomalyEvents.onStartHoldingAnomaly += StartHoldingAnomaly;
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly += UndoAnomaly;
        Debug.Log("Subscribe to game events");
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly -= UndoAnomaly;
        GameEventsManager.instance.anomalyEvents.onStartHoldingAnomaly -= StartHoldingAnomaly;
    }

    public abstract bool TriggerAnomaly();
    public abstract void UndoAnomaly();

    public abstract void StartHoldingAnomaly();
}
