using UnityEngine;

public class AnimEventHelper : MonoBehaviour
{
    [SerializeField] string eventName;
    private void FinishAnimation()
    {
        GameEventsManager.instance.anomalyEvents.FinishAnimationEvent(eventName);
    }

}
