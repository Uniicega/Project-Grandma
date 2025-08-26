using UnityEngine;

[System.Serializable]
public class TimedEnemyBehavior
{
    [Header("Enemy Event")]
    public TestEnemy2 enemy;
    public float eventTime;

    [Header("Update AI Config")]
    public int newDifficultyLevel;
    public float newCooldownDuration;

    public void UpdateEnemyAI() //Set new parameter for the enemy Ai
    {
        enemy.difficultyLevel = newDifficultyLevel;
        enemy.cooldownDuration = newCooldownDuration;
        Debug.Log("Update Enemy AI At Time: " + eventTime);
    }
}
