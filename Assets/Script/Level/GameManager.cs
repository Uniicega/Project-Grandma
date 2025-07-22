using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Level Config")]
    public float second;
    public float timeLimit;
    public float tickRate;
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI debugMessage;

    [Header("Enemy Event")]
    public List<EnemyEvent> enemyEvents = new List<EnemyEvent>();
    int eventIndex = 0;

    private void Update()
    {
        UpdateTime(); //Counting up time
        DisplayTime(); //Display time on screem
        CheckEnemyEvent(); //Do time based enemy event
    }

    private void UpdateTime()
    {
        second += Time.fixedDeltaTime * tickRate;
    }

    private void DisplayTime()
    {
        timeDisplay.text = second.ToString();
    }

    private void CheckEnemyEvent() 
    {
        if(eventIndex < enemyEvents.Count)
        {
            if (enemyEvents[eventIndex].eventTime <= second) //Check if it's time for next enemy event to happen
            {
                enemyEvents[eventIndex].UpdateEnemyAI(); //Update enemy Ai using method in EnemyEvent class
                eventIndex++; //Move on to the next enemy event
                debugMessage.text = "Change enemy AI at time: " + second.ToString();
            }
        }       
    }
}
