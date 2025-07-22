using UnityEngine;

[System.Serializable]
public class EnemyEvent
{
    [Header("Enemy Event")]
    public EnemyMoveBehavior enemy;
    public float eventTime;

    [Header("Update AI Config")]
    public int newDifficultyLevel;
    public bool newWeightedBehavior;
    public float newOpportunityTime;

    public void UpdateEnemyAI() //Set new parameter for the enemy Ai
    {
        enemy.difficultyLevel = newDifficultyLevel;
        enemy.weightedBehavior = newWeightedBehavior;
        enemy.opportunityTime = newOpportunityTime;
        Debug.Log("Update Enemy AI: ");
    }
}
