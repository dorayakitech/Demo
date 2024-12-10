using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(InteractableFlashVFX))]
public class PressedPlate : MonoBehaviour
{
    [SerializeField, Required] private Transform _checkPoint;

    private InteractableFlashVFX _flashVFX;
    private readonly List<Collider> _pressers = new();

    private void Awake()
    {
        _flashVFX = GetComponent<InteractableFlashVFX>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _flashVFX.StartFlash(InteractableType.PressedPlate);

        if (!_pressers.Contains(other))
            _pressers.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_pressers.Contains(other))
            _pressers.Remove(other);

        if (_pressers.Count == 0)
            _flashVFX.StopFlash(InteractableType.PressedPlate);
    }
}