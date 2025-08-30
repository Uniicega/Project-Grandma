using UnityEngine;

public class AppearingAnomaly : Anomaly
{
    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material; //Save default object material
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public override void TriggerAnomaly()
    {
        Debug.Log("Triggered Appearing Anomaly: " + this.name);
        isActive = true;
        cooldown = cooldownTimer;
        currentAnomalyPoint = anomalyPointValue;

        gameObject.GetComponent<MeshRenderer>().enabled = true; //Make the object appear
        gameObject.GetComponent<Collider>().enabled = true; //Make the object appear
    }

    public override void UndoAnomaly(Anomaly anomaly)
    {
        if (anomaly == this && isActive)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false; //Make anomaly dissapear
            gameObject.GetComponent<Collider>().enabled = false;

            currentAnomalyPoint = 0;
            isActive = false;
        }
    }
}
