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

    [Header("State")]
    [SerializeField] private float currentCooldown;
    [SerializeField] private float currentGrace;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int anomalyPoint;

    private AnomalyManager anomalyManager;
    
    

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
            currentGrace -= Time.deltaTime;
        }
    }

    private void TrySpawningAnomaly()
    {
        anomalyPoint = anomalyManager.TallyAnomalyPoint();

        if (difficultyLevel >= Random.Range(0, 20))
        {
            if (anomalyPoint >= attackThreashhold && currentGrace <= 0)
            {
                AttackPlayer();
            }
            else if (anomalyPoint >= heavyAnomalyThreshhold)
            {
                if (!anomalyManager.TriggerRandomHeavyAnomaly())
                    currentCooldown = 0;
                else currentCooldown = cooldownDuration;
            }
            else
            {
                if (!anomalyManager.TriggerRandomLightAnomaly())
                    currentCooldown = 0;
                else currentCooldown = cooldownDuration;
            }
            return;
        }
        currentCooldown = cooldownDuration;
    }

    private void AttackPlayer()
    {
        GameEventsManager.instance.anomalyEvents.SnapIncense();
        currentGrace = graceDuration;
    }
}
