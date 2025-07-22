using UnityEngine;

public abstract class Anomaly: MonoBehaviour
{
    //Abstract class for anomalies
    public int currentAnomalyPoint;
    public int anomalyValue;
    public abstract bool TriggerAnomaly();
    public abstract void UndoAnomaly();
}
