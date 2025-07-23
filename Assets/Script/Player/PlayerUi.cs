using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public GameObject sliderObject;
    public Slider slider;
    private float timer;
    private bool isHolding = false;

    private void Start()
    {
        slider = sliderObject.GetComponent<Slider>();
        sliderObject.SetActive(false);
    }

    private void Update()
    {
        if( Input.GetMouseButton(0) )
        {
            ActivateSlider(); //Activate slider
            timer += Time.deltaTime; //Counting up slider timer
            slider.value = timer;

            if( timer >= slider.maxValue && sliderObject.activeSelf) //If timer is complete and is still active (this is to stop the slider from reactivating again without letting go of the mouse)
            {
                sliderObject.SetActive(false); //Deactivate slider
                GameEventsManager.instance.anomalyEvents.UndoAnomaly();
            }
        }
        else if( Input.GetMouseButtonUp(0) ) //If player release mouse button, deactivate slider 
        {
            sliderObject.SetActive(false );
            isHolding = false;
        }
    }
    private void ActivateSlider()
    {
        if (!isHolding)
        {
            GameEventsManager.instance.anomalyEvents.StartHoldingAnomaly();
            timer = 0; //Reset slider timer
            isHolding = true; //Keep track when mosue is already been held

            sliderObject.SetActive(true); //Activate slider object
            Vector2 mousePosition = Input.mousePosition;
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, mousePosition, canvas.worldCamera, out uiPosition); //Position magic to get canvas position of the mouse
            slider.transform.position = canvas.transform.TransformPoint(uiPosition); //Teleport slider to the mouse position
        }

    }
}
