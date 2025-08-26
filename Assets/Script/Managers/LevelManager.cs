using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Level Config")]
    public float currentTime;
    [SerializeField] private float midnightTime = 120;
    public float finishTime = 360;
    [SerializeField] public int timeSpeed = 1;

    [Header("Incense Config")]
    [SerializeField] private float incenseCurrentTime;
    [SerializeField] private Incense incense;
    [SerializeField] private float incenseMaxTime;
    [SerializeField] private int incenseSection;
    [SerializeField] private int maxIncenseSection;

    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI debugMessage;
    public GameObject VictoryMessage;
    public GameObject DefeatMessage;

    int hour;
    int minute;
    float size;


    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onRefillIncense += RefillIncense;
        GameEventsManager.instance.anomalyEvents.onSnapIncense += SnapIncense;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onRefillIncense -= RefillIncense;
        GameEventsManager.instance.anomalyEvents.onSnapIncense -= SnapIncense;
    }
    private void Start()
    {
        incenseSection = maxIncenseSection;
        incenseCurrentTime = incenseMaxTime;
    }

    private void Update()
    {
        UpdateTime();
        DisplayTime();

        SetIncenseSize();
        CheckVictory();
        CheckDefeat();
    }

    private void UpdateTime()
    { 
        currentTime += Time.deltaTime * timeSpeed;
        incenseCurrentTime -=  Time.deltaTime * timeSpeed;
    }

    private void DisplayTime()
    {
        if (currentTime < midnightTime)
        {
            hour = 22 + (int)Math.Floor(currentTime / 60);
        }
        else if (currentTime >= midnightTime)
        {
            hour = (int)Math.Floor((currentTime - 120) / 60);
        }
        minute = (int)Math.Floor(currentTime % 60 / 10);


        if (currentTime < midnightTime)
        {
            timeDisplay.text = hour.ToString() + " : " + minute.ToString() + "0";
        }
        else if (currentTime >= midnightTime)
        {
            timeDisplay.text = "0" + hour.ToString() + " : " + minute.ToString() + "0";
        }
        
    }

    //---------------------Incense functions----------------------------
    private void SetIncenseSize()
    {
        float incensePercentage =  incenseCurrentTime / 100;
        incense.incensePercentage = incensePercentage;
    }

    private void SnapIncense()
    {
        incenseSection--;
        SetIncenseSection();
    }

    private void SetIncenseSection()
    {
        size = (float)incenseSection / maxIncenseSection;
        incenseMaxTime = incenseMaxTime * size;
        if (incenseCurrentTime > incenseMaxTime)
        {
            incenseCurrentTime = incenseMaxTime;
        }
        Debug.Log(incenseCurrentTime + "-" + incenseMaxTime);
        
    }

    private void RefillIncense()
    {
        incenseCurrentTime = incenseMaxTime;
    }

    //----------------------------------------------------------------
    

    private void CheckVictory()
    {
        if(currentTime >= finishTime)
        {
            VictoryMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void CheckDefeat()
    {
        if(incenseCurrentTime <= 0)
        {
            Debug.Log("Defeat");
        }
    }


}
