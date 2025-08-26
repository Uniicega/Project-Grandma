using System.Collections.Generic;
using UnityEngine;

public class TestEnemy2 : MonoBehaviour
{
    [Header("Anomalies")]
    public int difficultyLevel;
    [SerializeField] AnomalyManager anomalyManager;
    public int heavyAnomalyThreshhold;
    public int attackThreashhold;
    public float cooldownDuration;
    public float graceDuration;

    private float currentCooldown;
    private bool isAttaching;

    private void Update()
    {
        if(currentCooldown <= 0)
        {
            TrySpawningAnomaly();
            currentCooldown = cooldownDuration;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void TrySpawningAnomaly()
    {
        int anomalyPoint = anomalyManager.TallyAnomalyPoint();

        if (difficultyLevel >= Random.Range(0, 20))
        {
            if(anomalyPoint >= heavyAnomalyThreshhold)
            {
                if (!anomalyManager.TriggerRandomHeavyAnomaly())
                    currentCooldown = 0;
                else currentCooldown = cooldownDuration;
            }
            else if(anomalyPoint <= cooldownDuration)
            {
                if (!anomalyManager.TriggerRandomLightAnomaly())
                    currentCooldown = 0;
                else currentCooldown = cooldownDuration;
            }
            else if(anomalyPoint >= attackThreashhold)
            {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        GameEventsManager.instance.anomalyEvents.SnapIncense();
    }
}
