using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class PressedPlate : MonoBehaviour
{
    [SerializeField, Required] private Transform _checkPoint;

    private InteractableFlashVFX _flashVFX;
    private bool _isPressed;

    private void Awake()
    {
        _flashVFX = GetComponent<InteractableFlashVFX>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _flashVFX.StartFlash(InteractableType.PressedPlate);
    }

    private void OnTriggerExit(Collider other)
    {
        _flashVFX.StopFlash(InteractableType.PressedPlate);
    }
}