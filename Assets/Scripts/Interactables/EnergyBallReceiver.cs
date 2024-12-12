using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class EnergyBallReceiver : SerializedMonoBehaviour, IInteractable, IActivate
{
    [SerializeField, Required] private List<ICommand> _tasksAfterActivated = new();

    private InteractableFlashVFX _flashVFX;
    private List<MeshRenderer> _meshRenderers;
    protected List<Material> currentMaterials;

    public GameObject Obj => gameObject;

    private void Awake()
    {
        _flashVFX = GetComponent<InteractableFlashVFX>();
        MaterialChanger.FindMeshRenderers(transform, out _meshRenderers, out currentMaterials);
    }

    public void IsDetected()
    {
        _flashVFX.StartFlash(InteractableType.EnergyBallReceiver, _meshRenderers);
    }

    public void IsUndetected()
    {
        _flashVFX.StopFlash(_meshRenderers, currentMaterials);
    }

    public void IsSetTarget()
    {
    }

    public void IsUnsetTarget()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag(VariableNamesDefine.EnergyTag) || !enabled) return;
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