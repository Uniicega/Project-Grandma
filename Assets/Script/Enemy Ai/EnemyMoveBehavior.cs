using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBehavior : MonoBehaviour
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

    private void Start()
    {
        currentNode = starterNode;
        transform.position = currentNode.transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (opportunityTime <= timer)
        {
            HandleMovement();
            transform.position = currentNode.transform.position;
            timer = 0;
        }
    }

    private void HandleMovement()
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

    private void SelectRandomNeighbor()
    {
        availableNeighbor = currentNode.neighborNodes;
        int random = Random.Range(0, availableNeighbor.Count);
        currentNode = availableNeighbor[random];
    }

    private void SelectWeightedNeighbor()
    {
        availableNeighbor = currentNode.neighborNodes;

        int maxNodeValue = -1;
        List<MovementNode> maxNodePositions = new List<MovementNode>();

        for(int i = 0; i < availableNeighbor.Count; i++)
        {
            if (availableNeighbor[i].nodeValue > maxNodeValue)
            {
                maxNodeValue = availableNeighbor[i].nodeValue;
                maxNodePositions.Clear();
                maxNodePositions.Add(availableNeighbor[i]);
            }
            else if (availableNeighbor[i].nodeValue == maxNodeValue)
            {
                maxNodePositions.Add(availableNeighbor[i]);
            }
        }

        int random = Random.Range(0, maxNodePositions.Count);
        currentNode = maxNodePositions[random];
    }
}