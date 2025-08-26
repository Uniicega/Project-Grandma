using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] AnomalyManager anomalyManager;
    PlayerManager PlayerManager;
    UiManager uiManager;

    private void Start()
    {
        anomalyManager.finishTime = levelManager.finishTime;
    }

    private void Update()
    {
        anomalyManager.currentTime = levelManager.currentTime;
    }
}
