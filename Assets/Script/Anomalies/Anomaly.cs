using TMPro;
using UnityEngine;

public abstract class Anomaly: MonoBehaviour
{
    [Header("Anomaly Config")]
    public AnomalyEnum anomalyEnum;
    public int anomalyPointValue;
    public int cooldownTimer = 10;

    [Header("Anomaly State")]
    public bool isActive;
    public int currentAnomalyPoint;

    protected float cooldown;

    protected Material originalMaterial;
    protected Material currentMaterial;
    protected bool currentMeshActive;
    public Material highlightMaterial;
    protected bool isHighlighted = false; 
    
    protected GameObject playerCam;


    private void OnEnable()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player");

        GameEventsManager.instance.anomalyEvents.onUndoAnomaly += UndoAnomaly;

        GameEventsManager.instance.debugEvents.onPressHighlight += PressHighlight;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly -= UndoAnomaly;

        GameEventsManager.instance.debugEvents.onPressHighlight -= PressHighlight;
    }

    protected void Update()
    {
        if (cooldown > 0) //Counting down the cooldown
        {
            cooldown -= Time.deltaTime;
        }
    }

    public bool SpawnAnomaly()
    {
        if (cooldown <= 0 && !isActive && CheckPlayerIsNotLooking()) //Check if not in cooldown and not already actived
        {
            TriggerAnomaly();
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract void TriggerAnomaly();

    public abstract void UndoAnomaly(Anomaly anomaly);


    public bool CheckPlayerIsNotLooking() //Calculate if the player is looking at the node or not
    {
        Vector3 dir = Vector3.Normalize(this.transform.position - playerCam.transform.position);
        float dot = Vector3.Dot(dir, playerCam.transform.forward);

        if (dot >= 0.5)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    

    //---------------------Debug functions ------------------------------

    public void ActivateLightAnomalies()
    {
        if (anomalyEnum == AnomalyEnum.HeavyAnomaly)
        {
            SpawnAnomaly();
        }
    }

    public void ActivateHeavyAnomalies()
    {
        if (anomalyEnum == AnomalyEnum.HeavyAnomaly)
        {
            SpawnAnomaly();
        }
    }

    public void ActivateAllAnomalies()
    {
        SpawnAnomaly();
    }

    public void PressHighlight()
    {
        if (!isHighlighted && isActive)
        {
            isHighlighted = true;
            currentMeshActive = GetComponent<MeshRenderer>().enabled;
            if (!currentMeshActive)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            currentMaterial = GetComponent<MeshRenderer>().material;
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else if (isHighlighted)
        {
            isHighlighted = false;
            gameObject.GetComponent<MeshRenderer>().material = currentMaterial;
            gameObject.GetComponent<MeshRenderer>().enabled = currentMeshActive;
            Debug.Log(this.gameObject.name + "mesh state was : " + currentMeshActive);
        }
    }
}
