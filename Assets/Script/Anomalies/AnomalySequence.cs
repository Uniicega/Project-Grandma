using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnomalySequence : Anomaly
{
    [Header("Sequence Config")]
    [SerializeField] private List<Anomaly> anomalyList;
    [SerializeField] float[] delayTime;

    private int index = 0;

    public override void TriggerAnomaly()
    {
        anomalyList[index].TriggerAnomaly();
        Debug.Log("Trigger Anomaly List: " + this.name + index);
    }

    public override void UndoAnomaly(Anomaly anomaly)
    {  
        if(anomaly == anomalyList[index])
        {
            index++;
            if(anomalyList[index] != null)
            {
                Invoke("ContinueAnomaly", delayTime[index]);
            }
            else
            {
                Debug.LogWarning("Anomaly Sequence List Index OutOfRange: " + this.name);
            }
        }
    }

    private void ContinueAnomaly()
    {
        if (index <= anomalyList.Count)
        {
            anomalyList[index].TriggerAnomaly();
        }
    }
}