using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AnomalyManager : MonoBehaviour
{
    [Header("State")]
    public int anomalyPoint;
    public float currentTime;
    public float finishTime;

    [Header("Enemy Event")]
    public List<TimedEnemyBehavior> enemyEvents = new List<TimedEnemyBehavior>();
    int eventIndex = 0;
    float nextEventTime;

    private List<Anomaly> LightAnomalies = new List<Anomaly>();
    private List<Anomaly> HeavyAnomalies = new List<Anomaly>();


    private void Start()
    {
        Anomaly[] anomalies = FindObjectsByType<Anomaly>(FindObjectsSortMode.None);

        if(anomalies != null)
        {
            foreach (Anomaly anomaly in anomalies) //Assign all anomaly into a list by type
            {
                if (anomaly.anomalyEnum == AnomalyEnum.LightAnomaly)
                {
                    LightAnomalies.Add(anomaly);
                }
                else if (anomaly.anomalyEnum == AnomalyEnum.HeavyAnomaly)
                {
                    HeavyAnomalies.Add(anomaly);
                }
                else
                {
                    Debug.LogWarning("Anomaly Not Assigned Type: " + anomaly.name);
                }
            }
        }
        
    }
    private void Update()
    {
        if(enemyEvents != null)
        {
            CheckEnemyEvent();
        }
        TallyAnomalyPoint();
    }

    private void CheckEnemyEvent()
    {
        if (eventIndex < enemyEvents.Count)
        {
            if (nextEventTime <= currentTime)
            {
                enemyEvents[eventIndex].UpdateEnemyAI(); //Update enemy Ai using method in EnemyEvent class
                eventIndex++; 
                nextEventTime = enemyEvents[eventIndex].eventTime;
                Debug.Log("Change enemy AI at time: " + currentTime);
            }
        }
    }

    public bool TriggerRandomLightAnomaly()//Randomly trigger a light anomaly
    {
        int random = Random.Range(0, LightAnomalies.Count);
        if (LightAnomalies[random].SpawnAnomaly() == true)
        {
            GameEventsManager.instance.anomalyEvents.TriggerLightAnomaly();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TriggerRandomHeavyAnomaly()//Randomly trigger a light anomaly
    {
        int random = Random.Range(0, HeavyAnomalies.Count);
        if (HeavyAnomalies[random].SpawnAnomaly() == true)
        {
            GameEventsManager.instance.anomalyEvents.TriggerHeavyAnomaly();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int TallyAnomalyPoint() //Loop through all anomalies connected to this node and sum up the anomaly point
    {
        anomalyPoint = 0;
        if(LightAnomalies != null)
        {
            foreach (var anomaly in LightAnomalies)
            {
                anomalyPoint += anomaly.currentAnomalyPoint;
            }
        }
        if (HeavyAnomalies != null)
        {
            foreach (var anomaly in HeavyAnomalies)
            {
                anomalyPoint += anomaly.currentAnomalyPoint;
            }
        }
        return anomalyPoint;
        //valueText.text = anomalyPoint.ToString(); //Display point for debugging purpose
    }
}
