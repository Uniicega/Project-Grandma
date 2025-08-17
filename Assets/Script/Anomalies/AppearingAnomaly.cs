using UnityEngine;

public class AppearingAnomaly : Anomaly
{
    //Anomaly class for anomalies that make objects appear

    [Header("Config")]
    public int cooldownTimer = 10;
 
    public bool mouseIsOver = false;
    public bool undoValid;
    public float cooldown;


    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material; //Save default object material
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (cooldown > 0) //Counting down the cooldown
        {
            cooldown -= Time.deltaTime;
        }


    }

    private void OnMouseEnter()
    {
        mouseIsOver = true;
    }

    private void OnMouseExit()
    {
        mouseIsOver = false;
    }

    public override bool TriggerAnomaly()
    {
        if (cooldown <= 0 && !isActive) //Check if not in cooldown and not already actived
        {
            isActive = true; //Keep track of when it's active
            Debug.Log("Triggered appearing anomaly");

            gameObject.GetComponent<MeshRenderer>().enabled = true; //Make the object appear
            cooldown += cooldownTimer; //Add cooldown
            currentAnomalyPoint = anomalyValue; //Increase the anomaly point to the set value
            Debug.Log("Added anomaly point: " + currentAnomalyPoint);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void UndoAnomaly()
    {
        if(undoValid)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false; //Make anomaly dissapear
            currentAnomalyPoint = 0; //Remove anomaly point
            isActive = false;
        }       
    }

    public override void StartHoldingAnomaly()
    {
        if (mouseIsOver)
        {
            undoValid = true;
        }
        else { undoValid = false; }
    }
}
