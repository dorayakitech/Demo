using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class EnergyBallReceiver : MonoBehaviour, IInteractable
{
    private InteractableFlashVFX _flashVFX;

    public GameObject Obj => gameObject;

    private void Awake()
    {
        _flashVFX = GetComponent<InteractableFlashVFX>();
    }

    public void IsDetected()
    {
        _flashVFX.StartFlash(InteractableType.EnergyBallReceiver);
    }

    public void IsUndetected()
    {
        _flashVFX.StopFlash(InteractableType.EnergyBallReceiver);
    }

    public void IsSetTarget()
    {
        Debug.Log($"{gameObject.name} Is set target");
    }

    public void IsUnsetTarget()
    {
        Debug.Log($"{gameObject.name} Is unset target");
    }
}