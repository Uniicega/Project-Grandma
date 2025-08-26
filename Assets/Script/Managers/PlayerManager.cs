using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public GameObject anomalySliderObject;
    public GameObject prayingSliderObject;
    private Slider anomalySlider;
    private Slider prayingSlider;

    private float fixProgression;
    private float prayingProgression;
    private bool isHoldingFixAnomaly = false;
    private bool isPraying = false;

    public Anomaly currentAnomaly;
    public bool isLookingAtIncense;
    private bool isLighting;

    [SerializeField] Animator anomalyHandAnimator;
    [SerializeField] Animator lighterHandAnimator;

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
    }

    private void HandleAnomalySlider()
    {
        if (Input.GetMouseButton(0))
        {
            ActivateAnomalySlider(); //Activate slider

            fixProgression += Time.deltaTime;
            anomalySlider.value = fixProgression;

            if (fixProgression >= anomalySlider.maxValue && anomalySliderObject.activeSelf) //If timer is complete and is still active (this is to stop the slider from reactivating again without letting go of the mouse)
            { 
                if(isLighting)
                {
                    GameEventsManager.instance.playerEvents.RefilIncense();
                }
                else if (currentAnomaly != null)
                {
                    GameEventsManager.instance.anomalyEvents.UndoAnomaly(currentAnomaly.name);
                }
                anomalySliderObject.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonUp(0)) //If player release mouse button, deactivate slider 
        {          
            anomalySliderObject.SetActive(false);
            isHoldingFixAnomaly = false;

            if (isLighting)
            {
                lighterHandAnimator.SetTrigger("AnomalyHandDown");
            }
            else
            {
                anomalyHandAnimator.SetTrigger("AnomalyHandDown");
            }
            isLighting = false;
        }
    }

    private void ActivateAnomalySlider()
    {
        if (!isHoldingFixAnomaly)
        {
            GameEventsManager.instance.anomalyEvents.StartHoldingAnomaly();
            fixProgression = 0; //Reset slider timer
            isHoldingFixAnomaly = true; //Keep track when mosue is already been held

            isLighting = isLookingAtIncense;
            if (isLighting)
            {
                lighterHandAnimator.SetTrigger("AnomalyHandUp");
            }
            else
            {
                anomalyHandAnimator.SetTrigger("AnomalyHandUp");
            }          
            anomalySliderObject.SetActive(true);

            Vector2 mousePosition = Input.mousePosition;
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, mousePosition, canvas.worldCamera, out uiPosition); //Position magic to get canvas position of the mouse
            anomalySlider.transform.position = canvas.transform.TransformPoint(uiPosition); //Teleport slider to the mouse position
        }
    }

    //--------------------unused below----------------------------

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
    
}
