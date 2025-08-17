using UnityEngine;

public abstract class Anomaly: MonoBehaviour
{
    //Abstract class for anomalies
    [Header("Anomaly Config")]
    public bool lightAnomaly;
    public bool heavyAnomaly;
    public int currentAnomalyPoint;
    public int anomalyValue;
    protected bool isActive;

    protected Material originalMaterial;
    protected Material currentMaterial;
    protected bool currentMeshActive;
    public Material highlightMaterial;
    protected bool isHighlighted = false;

    private void OnEnable()
    {
        Invoke("SubscribeToEvents", 0.1f);
    }

    private void SubscribeToEvents()
    {
        GameEventsManager.instance.anomalyEvents.onStartHoldingAnomaly += StartHoldingAnomaly;
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly += UndoAnomaly;

        GameEventsManager.instance.debugEvents.onPressHighlight += PressHighlight;
        GameEventsManager.instance.debugEvents.onActivateAllAnomalies += ActivateAllAnomalies;
        GameEventsManager.instance.debugEvents.onActivateLightAnomalies += ActivateLightAnomalies;
        GameEventsManager.instance.debugEvents.onActivateHeavyAnomalies += ActivateHeavyAnomalies;
        Debug.Log("Subscribe to game events");
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly -= UndoAnomaly;
        GameEventsManager.instance.anomalyEvents.onStartHoldingAnomaly -= StartHoldingAnomaly;

        GameEventsManager.instance.debugEvents.onPressHighlight -= PressHighlight;
        GameEventsManager.instance.debugEvents.onActivateAllAnomalies -= ActivateAllAnomalies;
        GameEventsManager.instance.debugEvents.onActivateLightAnomalies -= ActivateLightAnomalies;
        GameEventsManager.instance.debugEvents.onActivateHeavyAnomalies -= ActivateHeavyAnomalies;
    }

    public abstract bool TriggerAnomaly();

    public abstract void UndoAnomaly();

    public abstract void StartHoldingAnomaly();

    public void PressHighlight()
    {
        if(!isHighlighted && isActive)
        {
            isHighlighted = true;
            currentMeshActive = GetComponent<MeshRenderer>().enabled;
            if(!currentMeshActive )
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            currentMaterial = GetComponent<MeshRenderer>().material;
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else if(isHighlighted)
        {
            isHighlighted = false;
            gameObject.GetComponent<MeshRenderer>().material = currentMaterial;
            gameObject.GetComponent <MeshRenderer>().enabled = currentMeshActive;
            Debug.Log(this.gameObject.name + "mesh state was : " + currentMeshActive);
        }
    }

    public void ActivateAllAnomalies()
    {
        TriggerAnomaly();
    }

    public void ActivateLightAnomalies()
    {
        if (lightAnomaly)
        {
            TriggerAnomaly();
        }
    }

    public void ActivateHeavyAnomalies()
    {
        if (heavyAnomaly)
        {
            TriggerAnomaly();
        }
    }
}
