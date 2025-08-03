using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System;

public class MovementNode : MonoBehaviour
{
    [Header("Config")]
    public float nodeValue = 0;
    public List<MovementNode> neighborNodes;

    [Header("Anomalies")]
    public List<Anomaly> lightAnomaly;
    public List<Anomaly> heavyAnomaly;

    [SerializeField] TextMeshPro valueText;
    GameObject playerCam;
    public int anomalyPoint = 0;

    private void Start()
    {
        foreach (var node in neighborNodes) //Set nodes to connect to each others
        {
            if (!node.neighborNodes.Contains(this))
            node.AddNeighbor(this);
        }
        valueText.text = anomalyPoint.ToString();
        playerCam = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        TallyAnomalyPoint();
    }

    public void CalculatePlayerLookDir() //Calculate if the player is looking at the node or not
    {
        Vector3 dir = Vector3.Normalize(this.transform.position - playerCam.transform.position);
        float dot = Vector3.Dot(dir, playerCam.transform.forward);
        nodeValue = dot;
        valueText.text = nodeValue.ToString();
    }

    public void TallyAnomalyPoint() //Loop through all anomalies connected to this node and sum up the anomaly point
    {
        anomalyPoint = 0;
        foreach(var anomaly in lightAnomaly)
        {
            anomalyPoint += anomaly.currentAnomalyPoint;
        }
        foreach (var anomaly in heavyAnomaly)
        {
            anomalyPoint += anomaly.currentAnomalyPoint;
        }
       //valueText.text = anomalyPoint.ToString(); //Display point for debugging purpose
    }

    public void AddNeighbor(MovementNode neighbor) //Add neighbor to the list
    {
        neighborNodes.Add(neighbor); 
    }

    private void OnDrawGizmos() //Funny green line in inspect
    {
        Gizmos.color = Color.green;


        foreach (var node in neighborNodes)
        {
            if (node != null)
                Gizmos.DrawLine(transform.position, node.transform.position);

        }
    }
}
