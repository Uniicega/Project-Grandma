using System.Collections.Generic;
using UnityEngine;

public class TestEnemy2 : MonoBehaviour
{
    [Header("Anomalies")]
    public int difficultyLevel;
    public int heavyAnomalyThreshhold;
    public int attackThreashhold;
    public float cooldownDuration;
    public float graceDuration;
    [SerializeField] GameObject ghostPrefab;

    [Header("State")]
    [SerializeField] private float currentCooldown;
    [SerializeField] private float currentGrace;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int anomalyPoint;

    private AnomalyManager anomalyManager;

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly += CheckFinishAttackAnomaly;
        GameEventsManager.instance.levelEvents.onPlayerDefeated += StartJumpscare;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly -= CheckFinishAttackAnomaly;
        GameEventsManager.instance.levelEvents.onPlayerDefeated -= StartJumpscare;

    }

    private void Start()
    {
        anomalyManager = GameManager.instance.anomalyManager;
    }

    private void Update()
    {
        if(currentCooldown <= 0)
        {
            TrySpawningAnomaly();
        }
        else
        {
            currentCooldown -= Time.deltaTime;
            if(currentGrace >= 0)
            {
                currentGrace -= Time.deltaTime;
            }      
        }
    }

    private void TrySpawningAnomaly()
    {
        anomalyPoint = anomalyManager.TallyAnomalyPoint();

        if (difficultyLevel >= Random.Range(0, 20) && currentGrace <= 0)
        { 
            if (anomalyPoint >= attackThreashhold)
            {
                anomalyManager.SpawnRandomAttackAnomaly();
                currentGrace = graceDuration;
            }

            else if (anomalyPoint >= heavyAnomalyThreshhold)
            {
                if (!anomalyManager.SpawnRandomHeavyAnomaly())
                {
                    currentCooldown = 0;
                    return;
                }    
            }
            else
            {
                if (!anomalyManager.SpawnRandomLightAnomaly())
                {
                    currentCooldown = 0;
                    return ;
                }
            }
        }
        currentCooldown = cooldownDuration;
    }

    private void CheckFinishAttackAnomaly(Anomaly anomaly)
    {
        if(anomaly.anomalyEnum == AnomalyEnum.AttackAnomaly)
        {
            GameEventsManager.instance.anomalyEvents.SnapIncense();
            GameManager.instance.anomalyManager.UndoAllAnomaly();
            currentGrace = graceDuration;
        }
    }

    private void StartJumpscare()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        ghost.GetComponent<ChaseJumpscareHandler>().StartJumpscare();
    }

}
