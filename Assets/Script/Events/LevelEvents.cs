using System;
using UnityEngine;

public class LevelEvents
{
    public event Action onPlayerVictory;
    public void PlayerVictory()
    {
        onPlayerVictory?.Invoke();
    }

    public event Action onPlayerDefeated;
    public void PlayerDefeated()
    {
        onPlayerDefeated?.Invoke();
    }
}
