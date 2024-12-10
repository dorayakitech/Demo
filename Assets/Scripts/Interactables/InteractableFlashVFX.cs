using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractableFlashVFX : MonoBehaviour
{
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _energyBallReceiverCfg;
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _gravityObjectCfg;
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _pressedPlateCfg;

    private readonly List<MeshRenderer> _meshRenderers = new();
    private int _activeGlowMaterialIndex;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        FindMeshRenderersRecursively(transform);
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
        foreach (var mr in _meshRenderers)
        {
            mr.materials = newMats;
        }
    }

    private void FindMeshRenderersRecursively(Transform parent)
    {
        if (parent.TryGetComponent<MeshRenderer>(out var mr))
            _meshRenderers.Add(mr);

        foreach (Transform child in parent)
        {
            FindMeshRenderersRecursively(child);
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