using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractableFlashVFX : MonoBehaviour
{
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _energyBallReceiverCfg;
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _gravityObjectCfg;
    [SerializeField, AssetsOnly] private SOInteractableFlashVFXConfig _pressedPlateCfg;

    private List<MeshRenderer> _meshRenderers = new();
    private List<Material> _defaultMaterials = new();
    private int _activeGlowMaterialIndex;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        MaterialChanger.FindMeshRenderersRecursively(transform, ref _meshRenderers, ref _defaultMaterials);
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
        ResetMaterials();
        _activeGlowMaterialIndex = 0;
    }

    private IEnumerator FlashCoroutine(SOInteractableFlashVFXConfig cfg)
    {
        while (true)
        {
            var newMat = cfg.GlowMaterials[_activeGlowMaterialIndex];
            MaterialChanger.ChangeMaterial(newMat, ref _meshRenderers);

            _activeGlowMaterialIndex += 1;
            if (_activeGlowMaterialIndex >= cfg.GlowMaterials.Count)
                _activeGlowMaterialIndex = 0;

            yield return new WaitForSeconds(cfg.TransitionDuration);
        }
    }

    private void ResetMaterials()
    {
        for (var i = 0; i < _meshRenderers.Count; i++)
        {
            var defaultMaterial = new[] { _defaultMaterials[i] };
            _meshRenderers[i].materials = defaultMaterial;
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