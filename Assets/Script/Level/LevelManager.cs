using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Level Config")]
    public float second;
    public float midnightSecond = 120;
    public float timeLimit = 360;
    public int timeSpeed = 1;

    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI debugMessage;
    public GameObject VictoryMessage;

    [Header("Enemy Event")]
    public List<EnemyEvent> enemyEvents = new List<EnemyEvent>();
    int eventIndex = 0;
    float nextEventTime;

    int hour;
    int minute;

    private void Start()
    {
        if (eventIndex < enemyEvents.Count)
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
        second += Time.deltaTime * timeSpeed;
    }

    private void DisplayTime()
    {
        ConvertSecondToHour();
        if (second < midnightSecond)
        {
            timeDisplay.text = hour.ToString() + " : " + minute.ToString() + "0";
        }
        else if (second >= midnightSecond)
        {
            timeDisplay.text = "0" + hour.ToString() + " : " + minute.ToString() + "0";
        }
        
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

    private void ConvertSecondToHour()
    {
        if(second < midnightSecond)
        {
            hour = 22 + (int)Math.Floor(second / 60);
        }
        else if(second >= midnightSecond)
        {
            hour = (int)Math.Floor((second - 120) / 60);
        }

        minute = (int)Math.Floor(second % 60 / 10);
    }
}
