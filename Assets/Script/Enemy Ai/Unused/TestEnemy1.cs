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

    [Header("Anomany")]
    public int lightAnomalyThreshhold;
    public int heavyAnomalyThreshhold;
    private float lightAnomalyCooldown = 2;
    private float heavyAnomalyCooldown = 2;
    private bool praySuccessed = false;

    [Header("Nodes")]
    [SerializeField]  private List<GameObject> gameobjectNodes;
    [SerializeField]  private List<MovementNode> movementNodes;
    public int anomalyPoints;

    private float attackCountdown = 10;
    bool onCooldown;
    float cooldownTimer;
    LevelManager manager;

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onFinishPraying += FinishPraying;
        GameEventsManager.instance.debugEvents.onActivateAttackAnomalies += ActivateAttackAnomaly;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        gameobjectNodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Nodes"));
        movementNodes = new List<MovementNode>();
        enemyEnum = EnemyEnum.HeavyAnomalyPhase;
        foreach (GameObject nodes in gameobjectNodes)
        {
            movementNodes.Add(nodes.GetComponent<MovementNode>());
        }
    }
    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onFinishPraying -= FinishPraying;
        GameEventsManager.instance.debugEvents.onActivateAttackAnomalies -= ActivateAttackAnomaly;

    }

    private void FixedUpdate()
    {
        lightAnomalyCooldown -= Time.deltaTime;
        heavyAnomalyCooldown -= Time.deltaTime;
        /*if (currentNode.lightAnomaly.Count > 0 && lightAnomalyCooldown <= 0) //Test, if there's light anomalies avaliable, randomly trigger one
        {
            TriggerRandomLightAnomaly();
        }
        if (currentNode.heavyAnomaly.Count > 0 && heavyAnomalyCooldown <= 0) //Test, if there's heavy anomalies avaliable, randomly trigger one
        {
            TriggerRandomHeavyAnomaly();
        }*/

        TallyAnomalyPoints();

        /*if(anomalyPoints >= heavyAnomalyThreshhold && enemyEnum == EnemyEnum.HeavyAnomalyPhase)
        {
            enemyEnum = EnemyEnum.AttackPhase;
        }

        if(enemyEnum == EnemyEnum.AttackPhase && praySuccessed)
        {
            enemyEnum = EnemyEnum.ChasedPhase; 
            praySuccessed = false;
        }

        if(enemyEnum != EnemyEnum.AttackPhase)
        {
            praySuccessed = false;
        }

        if(enemyEnum == EnemyEnum.HeavyAnomalyPhase)
        {
            HeavyAnomalyPhase();
        }
        else*/ if (enemyEnum == EnemyEnum.AttackPhase)
        {
            AttackPhase();
        }
        else if (enemyEnum == EnemyEnum.ChasedPhase)
        {
            ChasedPhase();
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

    private void TriggerRandomLightAnomaly()//Randomly trigger a light anomaly
    {
        List<Anomaly> lightAnomalies = currentNode.lightAnomaly;
        int random = Random.Range(0, lightAnomalies.Count);
        if(lightAnomalies[random].SpawnAnomaly() == true)
        {
            GameEventsManager.instance.anomalyEvents.TriggerLightAnomaly();
        }
        lightAnomalyCooldown = 2;
    }

    private void TriggerRandomHeavyAnomaly() //Randomly tirgger a heavy anomaly
    {
        List<Anomaly> HeavyAnomalies = currentNode.heavyAnomaly;
        int random = Random.Range(0, HeavyAnomalies.Count);
        if(HeavyAnomalies[random].SpawnAnomaly() == true)
        {
            GameEventsManager.instance.anomalyEvents.TriggerHeavyAnomaly();
        }
        heavyAnomalyCooldown = 2;
    }

    private void FinishPraying()
    {
        if(playerIsLooking)
        {
            praySuccessed = true;
        }
        else
        {
            praySuccessed = false;
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
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        GameEventsManager.instance.anomalyEvents.TriggerAttackAnomaly();
        timer = 0;
        attackCountdown -= Time.deltaTime;
        if(attackCountdown <= 0)
        {
            Debug.Log("Snap");
            attackCountdown = 10;
            enemyEnum = EnemyEnum.ChasedPhase;
            GameEventsManager.instance.anomalyEvents.TriggerChasedAnomaly();

        }
    }

    private void ChasedPhase()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void ActivateAttackAnomaly()
    {
        enemyEnum = EnemyEnum.AttackPhase;
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
