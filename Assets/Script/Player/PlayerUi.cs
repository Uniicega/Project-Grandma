using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public GameObject anomalySliderObject;
    public GameObject prayingSliderObject;
    public Slider anomalySlider;
    public Slider prayingSlider;
    private float fixProgression;
    private float prayingProgression;
    private bool isHoldingFixAnomaly = false;
    private bool isPraying = false;

    [SerializeField] Animator anomalyHandAnimator;

    private void Start()
    {
        anomalySlider = anomalySliderObject.GetComponent<Slider>();
        anomalySliderObject.SetActive(false);

        prayingSlider = prayingSliderObject.GetComponent<Slider>();
        prayingSliderObject.SetActive(false);
    }

    private void Update()
    {
        HandleAnomalySlider();
        HandlePrayingSlider();
        HandleDebugToggles();
    }

    private void HandleAnomalySlider()
    {
        if (Input.GetMouseButton(0))
        {
            ActivateAnomalySlider(); //Activate slider
            fixProgression += Time.deltaTime; //Counting up slider timer
            anomalySlider.value = fixProgression;

            if (fixProgression >= anomalySlider.maxValue && anomalySliderObject.activeSelf) //If timer is complete and is still active (this is to stop the slider from reactivating again without letting go of the mouse)
            {
                anomalySliderObject.SetActive(false); //Deactivate slider
                GameEventsManager.instance.anomalyEvents.UndoAnomaly();
            }
        }
        else if (Input.GetMouseButtonUp(0)) //If player release mouse button, deactivate slider 
        {
            anomalyHandAnimator.SetTrigger("AnomalyHandDown");
            anomalySliderObject.SetActive(false);
            isHoldingFixAnomaly = false;
        }
    }
    private void HandlePrayingSlider()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ActivatePrayingSldier();
            prayingProgression += Time.deltaTime;
            prayingSlider.value = prayingProgression;

            if (prayingProgression >= prayingSlider.maxValue && prayingSliderObject.activeSelf)
            {
                prayingSliderObject.SetActive(false);
                GameEventsManager.instance.anomalyEvents.FinishPraying();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            prayingSliderObject.SetActive(false);
            isPraying = false;
        }
    }
    private void ActivateAnomalySlider()
    {
        if (!isHoldingFixAnomaly)
        {
            anomalyHandAnimator.SetTrigger("AnomalyHandUp");
            GameEventsManager.instance.anomalyEvents.StartHoldingAnomaly();
            fixProgression = 0; //Reset slider timer
            isHoldingFixAnomaly = true; //Keep track when mosue is already been held

            anomalySliderObject.SetActive(true); //Activate slider object
            Vector2 mousePosition = Input.mousePosition;
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, mousePosition, canvas.worldCamera, out uiPosition); //Position magic to get canvas position of the mouse
            anomalySlider.transform.position = canvas.transform.TransformPoint(uiPosition); //Teleport slider to the mouse position
        }
    }
    private void ActivatePrayingSldier()
    {
        if (!isPraying)
        {
            //GameEventsManager.instance.anomalyEvents.StartHoldingAnomaly();
            prayingProgression = 0; //Reset slider timer
            isPraying = true; //Keep track when mosue is already been held

            prayingSliderObject.SetActive(true); //Activate slider object
        }
    }
    private void HandleDebugToggles()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameEventsManager.instance.debugEvents.PressHighlight();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameEventsManager.instance.debugEvents.ActivateLightAnomalies();
            GameEventsManager.instance.anomalyEvents.TriggerLightAnomaly();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEventsManager.instance.debugEvents.ActivateHeavyAnomalies();
            GameEventsManager.instance.anomalyEvents.TriggerHeavyAnomaly();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            GameEventsManager.instance.debugEvents.ActivateAttackAnomalies();
        }
    }
}
