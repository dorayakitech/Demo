using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class EnergyBallReceiver : SerializedMonoBehaviour, IInteractable, IActivate
{
    [SerializeField, Required] private List<ICommand> _tasksAfterActivated = new();

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
    }

    public void IsUnsetTarget()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag(VariableNamesDefine.EnergyTag)) return;
        Activate();
    }

    public void Activate()
    {
        foreach (var command in _tasksAfterActivated)
        {
            command.Execute(this);
        }
    }
}