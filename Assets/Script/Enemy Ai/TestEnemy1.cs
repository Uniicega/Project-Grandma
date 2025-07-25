using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public class TestEnemy1 : EnemyMoveBehavior
{
    [Header("Hightlight Materials")]
    Material originalMaterial;
    public Material highlightMaterial;
    public bool isHighlighted;
    [SerializeField] EnemyEnum enemyEnum;

    private float attackCountdown = 4;
    bool onCooldown;
    float cooldownTimer;
    LevelManager manager;

    [Header("Nodes")]
    [SerializeField]  private List<GameObject> gameobjectNodes;
    [SerializeField]  private List<MovementNode> movementNodes;
    public int anomalyPoints;

    private void OnEnable()
    {
        gameobjectNodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Nodes"));
        movementNodes = new List<MovementNode>();
        enemyEnum = EnemyEnum.LightAnomalyPhase;
        foreach (GameObject nodes in gameobjectNodes)
        {
            movementNodes.Add(nodes.GetComponent<MovementNode>());
        }
    }

    private void TallyAnomalyPoints()
    {
        anomalyPoints = 0;
        foreach (MovementNode nodes in movementNodes)
        {
            anomalyPoints += nodes.anomalyPoint;
        }
    }

    private void FixedUpdate()
    {
        TallyAnomalyPoints();

        if(anomalyPoints >= heavyAnomalyThreshhold)
        {
            AttackPhase();
        }
    }

    private void LightAnomalyPhase()
    {

    }

    private void HeavyAnomalyPhase()
    {

    }

    private void AttackPhase()
    {
        Debug.LogWarning("Enemy in attack phase");
        timer = 0;
        attackCountdown -= Time.deltaTime;
        if(attackCountdown <= 0)
        {
            Debug.Log("Player is dead");
            attackCountdown = 4;
        }
    }

    private void ChasedPhase()
    {

    }

    /*
    private void OnEnable()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            onCooldown = true;
        }
        else if (cooldownTimer <= 0) 
        {
            onCooldown = false; 
            weightedBehavior = false;

            if (Random.Range(1, 100) > 95 && Random.Range(1,5) > 3)
            {
                isHighlighted = true;
                gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;

                Debug.Log("Highlighted");
            }
        }


    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !onCooldown && isHighlighted)
        {
            Debug.LogWarning("Touching Enemy!");
            weightedBehavior = true;
            isHighlighted = false;
            cooldownTimer += 200;
        }
    }

    private void OnMouseEnter()
    {
        if (isHighlighted)
        {
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (isHighlighted)
        {
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = originalMaterial;

        }
    }*/
}
