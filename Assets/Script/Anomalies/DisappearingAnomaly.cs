using UnityEngine;

public class DisappearingAnomaly : Anomaly
{
    //Anomaly class for anomalies that make objects disappear

    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material; //Save default object material
    }

    public override void TriggerAnomaly()
    {
        Debug.Log("Triggered Disappearing Anomaly: " + this.name);
        isActive = true;
        cooldown = cooldownTimer;
        currentAnomalyPoint = anomalyPointValue;

        gameObject.GetComponent<MeshRenderer>().enabled = false; //Make the object disappear      
    }

    public override void UndoAnomaly(string id)
    {
        if(undoValid)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true; //Make anomaly appear back to normal
            currentAnomalyPoint = 0;
            isActive = false;
        }     
    }
}
