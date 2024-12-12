using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class PressedPlate : MonoBehaviour
{
    [SerializeField, Required] private Transform _checkPoint;

    private InteractableFlashVFX _flashVFX;
    private readonly List<Collider> _pressers = new();
    private List<MeshRenderer> _meshRenderers;
    private List<Material> _currentMaterials;

    private void Awake()
    {
        _flashVFX = GetComponent<InteractableFlashVFX>();
        MaterialChanger.FindMeshRenderers(transform, out _meshRenderers, out _currentMaterials);
    }

    private void OnTriggerEnter(Collider other)
    {
        _flashVFX.StartFlash(InteractableType.PressedPlate, _meshRenderers);

        if (!_pressers.Contains(other))
            _pressers.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_pressers.Contains(other))
            _pressers.Remove(other);

        if (_pressers.Count == 0)
            _flashVFX.StopFlash(_meshRenderers, _currentMaterials);
    }
}