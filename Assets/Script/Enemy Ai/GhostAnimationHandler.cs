using UnityEngine;

public class GhostAnimationHandler : MonoBehaviour
{
    [SerializeField]Ghost ghost;
    private void FinishTurningAround()
    {
        ghost.FinishTurningAround();
    }
}
