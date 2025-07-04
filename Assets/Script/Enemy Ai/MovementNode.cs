using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementNode : MonoBehaviour
{
    [Header("Config")]
    public int nodeValue = 0;
    public List<MovementNode> neighborNodes;

    public TextMeshPro valueText;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;


        foreach (var node in neighborNodes)
        {
            if(node != null)
            Gizmos.DrawLine(transform.position, node.transform.position);

        }       
    }

    private void Start()
    {
        foreach (var node in neighborNodes)
        {
            if (!node.neighborNodes.Contains(this))
            node.AddNeighbor(this);
        }

        valueText.text = nodeValue.ToString();
    }

    public void AddNeighbor(MovementNode neighbor)
    {
        neighborNodes.Add(neighbor);
    }
}
