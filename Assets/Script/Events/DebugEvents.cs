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
}
