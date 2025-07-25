using UnityEngine;
using System;

public class AnomalyEvents
{
    public event Action onUndoAnomaly;
    public void UndoAnomaly ()
    {
        if (onUndoAnomaly != null)
        {
            onUndoAnomaly();
        }
    }

    public event Action onStartHoldingAnomaly;
    public void StartHoldingAnomaly ()
    {
        if (onStartHoldingAnomaly != null)
        {
            onStartHoldingAnomaly();
        }
    }

    public event Action onTriggerLightAnomaly;
    public void TriggerLightAnomaly()
    {
        if (onTriggerLightAnomaly != null)
        {
            onTriggerLightAnomaly();
        }
    }

    public event Action onTriggerHeavyAnomaly;
    public void TriggerHeavyAnomaly()
    {
        if (onTriggerHeavyAnomaly != null)
        {
            onTriggerHeavyAnomaly();
        }
    }

    public event Action onTriggerAttackAnomaly;
    public void TriggerAttackAnomaly()
    {
        if (onTriggerAttackAnomaly != null)
        {
            onTriggerAttackAnomaly();
        }
    }

    public event Action onTriggerChasedAnomaly;
    public void TriggerChasedAnomaly()
    {
        if (onTriggerChasedAnomaly != null)
        {
            onTriggerChasedAnomaly();
        }
    }

    public event Action onFinishPraying;
    public void FinishPraying()
    {
        if (onFinishPraying != null)
        {
            onFinishPraying();
        }
    }
}
