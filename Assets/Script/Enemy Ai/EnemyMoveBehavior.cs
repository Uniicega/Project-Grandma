using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMoveBehavior : MonoBehaviour
{
    [Header("AI Config")]
    public int difficultyLevel;
    public bool weightedBehavior;
    public bool outOfSightBehavior;

    [Header("Anomany")]
    public int lightAnomalyThreshhold;
    public int heavyAnomalyThreshhold;

    [Header("Timer")]
    public float timer = 0;
    public float opportunityTime;

    [Header("Starter Node Ref")]
    public MovementNode starterNode;

    public MovementNode currentNode;
    List<MovementNode> availableNeighbor;
    Rigidbody rb;

    private void Start()
    {
        currentNode = starterNode;
        transform.position = currentNode.transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (opportunityTime <= timer) //See if enough time has passed for the enemy to have a movement oppertunity
        {
            HandleMovement();
            transform.position = currentNode.transform.position;
            timer = 0;
        }

        if (currentNode.heavyAnomaly.Count > 0) //Test, if there's heavy anomalies avaliable, randomly trigger one
        {
           TriggerRandomHeavyAnomaly();
        }
    }

    private void HandleMovement() //Randomly decide if the enemy get to move
    {
        if(difficultyLevel >= Random.Range(0, 20))
        {
            if (weightedBehavior)
            {
                SelectWeightedNeighbor();
            }
            else if (outOfSightBehavior)
            {
                SelectRandomOutOfSight();
            }
            else
            SelectRandomNeighbor();
        }
    }

    private void TriggerRandomLightAnomaly()//Randomly trigger a light anomaly
    {
        List<Anomaly> anomalies = currentNode.lightAnomaly;
        int random = Random.Range(0, anomalies.Count);
        anomalies[random].TriggerAnomaly();
    }

    private void TriggerRandomHeavyAnomaly() //Randomly tirgger a heavy anomaly
    {
        List<Anomaly> anomalies = currentNode.heavyAnomaly;
        int random = Random.Range(0, anomalies.Count);
        anomalies[random].TriggerAnomaly();
    }

    private void SelectRandomNeighbor() //Select a random node from the avaliable connected node
    {
        availableNeighbor = currentNode.neighborNodes;
        int random = Random.Range(0, availableNeighbor.Count);
        currentNode = availableNeighbor[random];
    }

    private void SelectRandomOutOfSight()
    {
        availableNeighbor = currentNode.neighborNodes;
        int random = Random.Range(0, availableNeighbor.Count);
        if (availableNeighbor[random].nodeValue > 0.9)
        {
            Debug.Log("Selection failed");
            SelectRandomOutOfSight();
        }
        else
        {
            Debug.Log("Selection Success");
            currentNode = availableNeighbor[random];
        }
    } //Same as SelectingRandomNode but exclude the node that player's looking at

    private void SelectWeightedNeighbor() //Make enemy move toward nodes with higher value
    {
        availableNeighbor = currentNode.neighborNodes;

        float maxNodeValue = -1;
        List<MovementNode> maxNodePositions = new List<MovementNode>();

        for(int i = 0; i < availableNeighbor.Count; i++)
        {
            if (availableNeighbor[i].nodeValue > maxNodeValue) //If the node value is higher, discard the lower value nodes and choose the new highest value node
            {
                maxNodeValue = availableNeighbor[i].nodeValue;
                maxNodePositions.Clear();
                maxNodePositions.Add(availableNeighbor[i]);
            }
            else if (availableNeighbor[i].nodeValue == maxNodeValue) //If the node value is the same as current highest value nod, keep both of them
            {
                maxNodePositions.Add(availableNeighbor[i]);
            }
        }

        int random = Random.Range(0, maxNodePositions.Count); //If there's more than 1 highest node value, pick a random one
        currentNode = maxNodePositions[random];
    }

}