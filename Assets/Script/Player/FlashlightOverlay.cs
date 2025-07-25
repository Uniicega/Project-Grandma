using UnityEngine;

public class FlashlightOverlay : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, mousePosition, canvas.worldCamera, out uiPosition); //Position magic to get canvas position of the mouse
        transform.position = canvas.transform.TransformPoint(uiPosition); //Teleport slider to the mouse position
    }
}
