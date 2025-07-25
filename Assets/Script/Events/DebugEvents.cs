using UnityEngine;
using System;

public class DebugEvents : MonoBehaviour
{
    public event Action onPressHighlight;
    public void PressHighlight()
    {
        if (onPressHighlight != null)
        {
            onPressHighlight();
        }
    }

    public event Action onActivateAllAnomalies;
    public void ActivateAllAnomalies()
    {
        if (onActivateAllAnomalies != null)
        {
            onActivateAllAnomalies();
        }
    }

    public event Action onActivateLightAnomalies;
    public void ActivateLightAnomalies()
    {
        if (onActivateLightAnomalies != null)
        {
            onActivateLightAnomalies();
        }
    }

    public event Action onActivateHeavyAnomalies;
    public void ActivateHeavyAnomalies()
    {
        if (onActivateHeavyAnomalies != null)
        {
            onActivateHeavyAnomalies();
        }
    }

    public event Action onActivateAttackAnomalies;
    public void ActivateAttackAnomalies()
    {
        if (onActivateAttackAnomalies != null)
        {
            onActivateAttackAnomalies();
        }
    }
}
