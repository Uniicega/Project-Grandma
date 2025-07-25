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

    public event Action onFinishPraying;
    public void FinishPraying()
    {
        if (onFinishPraying != null)
        {
            onFinishPraying();
        }
    }
}
