using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisappearingAnomaly : Anomaly
{
    //Anomaly class for anomalies that make objects disappear

    [Header("Config")]
    public int cooldownTimer = 150;

    Material originalMaterial;
    public Material highlightMaterial;
    public bool isActive;
    public bool mouseIsOver = false;
    public bool undoValid;
    public float cooldown;

    private void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material; //Save default object material
    }

    private void Update()
    {
       /* if (Random.Range(1, 100) > 98 && Random.Range(1, 5) > 3 && cooldown <= 0) //Randomly trigger anomaly when it's not on cooldown
        {
            TriggerAnomaly();
        }*/
        if(cooldown > 0) //Counting down the cooldown
        {
            cooldown -= Time.deltaTime;
        }
    }

    /*private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)) //Undo anomaly when clicked on
        {
            UndoAnomaly();
        }
    }*/

    private void OnMouseEnter()
    {
        /*if (isActive) //If anomaly is active, highlight the anomaly when hovering mouse over it
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }      */
        mouseIsOver = true;

    }

    private void OnMouseExit()
    {
        /*if (isActive) //Return material to its current state
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            //gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
        }*/
        mouseIsOver = false;
    }

    public override bool TriggerAnomaly()
    {
        Debug.Log("Triggered disappearing anomaly");
        if(cooldown <= 0 && !isActive) //Check if not in cooldown and not already actived
        {
            isActive = true; //Keep track of when it's active
            
            //gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;

            gameObject.GetComponent<MeshRenderer>().enabled = false; //Make the object disappear
            cooldown += cooldownTimer; //Add cooldown
            currentAnomalyPoint = anomalyValue; //Increase the anomaly point to the set value
            Debug.Log("current anomaly point: " +  currentAnomalyPoint);
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
            gameObject.GetComponent<MeshRenderer>().enabled = true; //Make anomaly appear back to normal
            currentAnomalyPoint = 0; //Remove anomaly point
            isActive = false;
        }     
    }

    public override void StartHoldingAnomaly()
    {
        if(mouseIsOver)
        {
            undoValid = true;
        }
        else { undoValid = false; }
    }
}
