using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Level Config")]
    public float second;
    public float timeLimit;
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI debugMessage;
    public GameObject VictoryMessage;

    [Header("Enemy Event")]
    public List<EnemyEvent> enemyEvents = new List<EnemyEvent>();
    int eventIndex = 0;
    float nextEventTime;

    private void Start()
    {
        nextEventTime = enemyEvents[eventIndex].eventTime;
    }

    private void Update()
    {
        UpdateTime(); //Counting up time
        DisplayTime(); //Display time on screen
        CheckVictory();
        CheckEnemyEvent(); //Do time based enemy event
    }

    private void UpdateTime()
    {
        second += Time.deltaTime;
    }

    private void DisplayTime()
    {
        timeDisplay.text = second.ToString();
    }

    private void CheckEnemyEvent() 
    {
        if(eventIndex < enemyEvents.Count)
        {
            if (nextEventTime <= second) //Check if it's time for next enemy event to happen
            {
                enemyEvents[eventIndex].UpdateEnemyAI(); //Update enemy Ai using method in EnemyEvent class
                eventIndex++; //Move on to the next enemy event
                nextEventTime = enemyEvents[eventIndex].eventTime;
                debugMessage.text = "Change enemy AI at time: " + second.ToString();
            }
        }       
    }

    private void CheckVictory()
    {
        if(second >= timeLimit)
        {
            VictoryMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
