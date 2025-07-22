using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMoveBehavior : MonoBehaviour
{
    [Header("AI Config")]
    public int difficultyLevel;
    public bool weightedBehavior;
    

    [Header("Timer")]
    public float timer = 0;
    public float opportunityTime;

    [Header("Starter Node Ref")]
    public MovementNode starterNode;

    MovementNode currentNode;
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
    }

    private void HandleMovement() //Randomly decide if the enemy get to move
    {
        if(difficultyLevel >= Random.Range(0, 20))
        {
            if (weightedBehavior)
            {
                SelectWeightedNeighbor();
            }
            else SelectRandomNeighbor();
        }
    }

    private void SelectRandomNeighbor() //Select a random node from the avaliable connected node
    {
        availableNeighbor = currentNode.neighborNodes;
        int random = Random.Range(0, availableNeighbor.Count);
        currentNode = availableNeighbor[random];
    }

    private void SelectWeightedNeighbor() //Make enemy move toward nodes with higher value
    {
        availableNeighbor = currentNode.neighborNodes;

        int maxNodeValue = -1;
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