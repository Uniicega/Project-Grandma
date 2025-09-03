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

    [SerializeField]private List<Anomaly> LightAnomalies = new List<Anomaly>();
    [SerializeField]private List<Anomaly> HeavyAnomalies = new List<Anomaly>();
    [SerializeField]private List<Anomaly> AttackAnomalies = new List<Anomaly>();

    [SerializeField] private List<Anomaly> ActiveAnomalies = new List<Anomaly>();

    private void OnEnable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly += UndoAnomaly;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.anomalyEvents.onUndoAnomaly -= UndoAnomaly;

    }

    private void Start()
    {
        CreateAnomalyLists();
    }

    public void CheckEnemyEvent()
    {
        if(enemyEvents != null)
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
    }

    public bool SpawnRandomLightAnomaly()//Randomly trigger a light anomaly
    {
        int random = Random.Range(0, LightAnomalies.Count);
        if(LightAnomalies.Count > 0)
        {
            if (LightAnomalies[random].SpawnAnomaly() == true)
            {
                GameEventsManager.instance.anomalyEvents.TriggerLightAnomaly();
                ActiveAnomalies.Add(LightAnomalies[random]);
                LightAnomalies.RemoveAt(random);
                return true;
            }
        }
        return false;
    }

    public bool SpawnRandomHeavyAnomaly()//Randomly trigger a heavy anomaly
    {
        int random = Random.Range(0, HeavyAnomalies.Count);
        if(HeavyAnomalies.Count > 0)
        {
            if (HeavyAnomalies[random].SpawnAnomaly() == true)
            {
                GameEventsManager.instance.anomalyEvents.TriggerHeavyAnomaly();
                ActiveAnomalies.Add(HeavyAnomalies[random]);
                HeavyAnomalies.RemoveAt(random);
                return true;
            }
        }
        return false;
    }

    public bool SpawnRandomAttackAnomaly()//Randomly trigger an attack anomaly
    {
        int random = Random.Range(0, AttackAnomalies.Count);
        if (AttackAnomalies.Count > 0)
        {
            if (AttackAnomalies[random].SpawnAnomaly() == true)
            {
                GameEventsManager.instance.anomalyEvents.TriggerAttackAnomaly();
                ActiveAnomalies.Add(AttackAnomalies[random]);
                AttackAnomalies.RemoveAt(random);
                return true;
            }
        }
        return false;
    }

    public int TallyAnomalyPoint() //Loop through all anomalies connected to this node and sum up the anomaly point
    {
        anomalyPoint = 0;
        foreach (Anomaly anomaly in ActiveAnomalies)
        {
            anomalyPoint += anomaly.anomalyPointValue;
        }
        return anomalyPoint;
    }

    private void UndoAnomaly(Anomaly anomaly)
    {
        if (!ActiveAnomalies.Contains(anomaly))
        { return; }

        if (anomaly.anomalyEnum == AnomalyEnum.LightAnomaly)
        {
            LightAnomalies.Add(anomaly);
            
        }
        else if (anomaly.anomalyEnum == AnomalyEnum.HeavyAnomaly)
        {
            HeavyAnomalies.Add(anomaly);
        }
        else if (anomaly.anomalyEnum == AnomalyEnum.AttackAnomaly)
        {
            AttackAnomalies.Add(anomaly);
        }
        else if (anomaly.anomalyEnum == AnomalyEnum.PartOfSequence)
        {

        }
        else
        {
            Debug.LogWarning("Anomaly Not Assigned Type: " + anomaly.name);
        }

        ActiveAnomalies.Remove(anomaly);
    }

    public void UndoAllAnomaly()
    {
        foreach (Anomaly anomaly in ActiveAnomalies)
        {
            if (anomaly.anomalyEnum == AnomalyEnum.LightAnomaly)
            {
                LightAnomalies.Add(anomaly);

            }
            else if (anomaly.anomalyEnum == AnomalyEnum.HeavyAnomaly)
            {
                HeavyAnomalies.Add(anomaly);
            }
            else if (anomaly.anomalyEnum == AnomalyEnum.AttackAnomaly)
            {
                AttackAnomalies.Add(anomaly);
            }
            else if (anomaly.anomalyEnum == AnomalyEnum.PartOfSequence)
            {

            }
            else
            {
                Debug.LogWarning("Anomaly Not Assigned Type: " + anomaly.name);
            }
        }
        ActiveAnomalies.Clear();
    }

    private void CreateAnomalyLists()
    {
        Anomaly[] anomalies = FindObjectsByType<Anomaly>(FindObjectsSortMode.None);

        if (anomalies != null)
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
                else if (anomaly.anomalyEnum == AnomalyEnum.AttackAnomaly)
                {
                    AttackAnomalies.Add(anomaly);
                }
                else if (anomaly.anomalyEnum == AnomalyEnum.PartOfSequence)
                {
                    
                }
                else
                {
                    Debug.LogWarning("Anomaly Not Assigned Type: " + anomaly.name);
                }
            }
        }
    }
}
