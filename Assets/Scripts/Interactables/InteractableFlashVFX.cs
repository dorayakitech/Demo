using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractableFlashVFX : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] private SOInteractableFlashVFXConfig _energyBallReceiverCfg;
    [SerializeField, Required, AssetsOnly] private SOInteractableFlashVFXConfig _gravityObjectCfg;
    [SerializeField, Required, AssetsOnly] private SOInteractableFlashVFXConfig _pressedPlateCfg;

    private MeshRenderer _meshRenderer;
    private readonly List<MeshRenderer> _childrenMeshRenderers = new();
    private int _activeGlowMaterialIndex;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        GetMeshRenderers();
    }

    public void StartFlash(InteractableType interactableType)
    {
        if (_flashCoroutine != null) return;

        var cfg = GetConfig(interactableType);
        _flashCoroutine = StartCoroutine(FlashCoroutine(cfg));
    }

    public void StopFlash(InteractableType interactableType)
    {
        if (_flashCoroutine == null) return;
        StopCoroutine(_flashCoroutine);
        _flashCoroutine = null;

        // reset material
        var cfg = GetConfig(interactableType);
        ChangeMaterial(cfg.DefaultMaterial);
        _activeGlowMaterialIndex = 0;
    }

    private IEnumerator FlashCoroutine(SOInteractableFlashVFXConfig cfg)
    {
        while (true)
        {
            var newMat = cfg.GlowMaterials[_activeGlowMaterialIndex];
            ChangeMaterial(newMat);

            _activeGlowMaterialIndex += 1;
            if (_activeGlowMaterialIndex >= cfg.GlowMaterials.Count)
                _activeGlowMaterialIndex = 0;

            yield return new WaitForSeconds(cfg.TransitionDuration);
        }
    }

    private void ChangeMaterial(Material mat)
    {
        var newMats = new[] { mat };

        if (_meshRenderer != null)
            _meshRenderer.materials = newMats;

        // handle child component
        foreach (var mr in _childrenMeshRenderers)
        {
            mr.materials = newMats;
        }
    }

    private void GetMeshRenderers()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out var mr))
                _childrenMeshRenderers.Add(mr);
        }
    }

    private SOInteractableFlashVFXConfig GetConfig(InteractableType interactableType)
    {
        return interactableType switch
        {
            InteractableType.EnergyBallReceiver => _energyBallReceiverCfg,
            InteractableType.GravityObject => _gravityObjectCfg,
            InteractableType.PressedPlate => _pressedPlateCfg,
            _ => null
        };
    }
}