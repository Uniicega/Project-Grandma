using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action onRefillIncense;
    public void RefilIncense()
    {
        onRefillIncense?.Invoke();
    }
}
