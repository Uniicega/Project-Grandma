using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public AnomalyEvents anomalyEvents;
    public DebugEvents debugEvents;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        anomalyEvents = new AnomalyEvents();
        debugEvents = new DebugEvents();
        Debug.Log("Instantiate game events");
    }
}
