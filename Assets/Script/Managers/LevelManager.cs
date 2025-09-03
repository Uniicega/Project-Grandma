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
    public float midnightTime = 120;
    public float finishTime = 360;
    [SerializeField] public int timeSpeed = 1;

    [Header("Incense Config")]
    [SerializeField] private float incenseCurrentTime;
    [SerializeField] private Incense incense;
    [SerializeField] private float incenseMaxTime;
    [SerializeField] private int incenseSection;
    [SerializeField] private int maxIncenseSection;

    public GameObject VictoryMessage;
    public GameObject DefeatMessage;

    bool isDefeated;
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
        SetIncenseSize();
        CheckVictory();
        CheckDefeat();

        GameManager.instance.anomalyManager.CheckEnemyEvent();
        GameManager.instance.anomalyManager.TallyAnomalyPoint();
    }

    private void UpdateTime()
    { 
        currentTime += Time.deltaTime * timeSpeed;
        incenseCurrentTime -=  Time.deltaTime * timeSpeed;
    }
    private void CheckVictory()
    {
        if (currentTime >= finishTime)
        {
            VictoryMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void CheckDefeat()
    {
        if (incenseCurrentTime <= 0 && !isDefeated)
        {
            isDefeated = true;
            timeSpeed = 0;
            GameEventsManager.instance.levelEvents.PlayerDefeated();
        }
    }

    public void FinishedDefeatAnim()
    {
        DefeatMessage.SetActive(true);
        Time.timeScale = 0;
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
    

    


}
