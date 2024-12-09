using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlashVFX : MonoBehaviour
{
    [SerializeField] private GameObject _meshRendererOwner;
    [SerializeField, Required, AssetsOnly] private List<Material> _glowMats;
    [SerializeField, Required, AssetsOnly] private Material _defaultMaterial;
    [SerializeField, Required] private float _transitionDuration = 2.0f;

    private MeshRenderer _meshRenderer;
    private List<MeshRenderer> _childrenMeshRenderers;
    private int _activeGlowMatIndex;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        if (_meshRendererOwner != null)
            GetMeshRenderers(_meshRendererOwner, out _meshRenderer, out _childrenMeshRenderers);
        else
            GetMeshRenderers(gameObject, out _meshRenderer, out _childrenMeshRenderers);
    }

    public void StartFlash()
    {
        if (_flashCoroutine != null) return;
        _flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    public void StopFlash()
    {
        if (_flashCoroutine == null) return;
        StopCoroutine(_flashCoroutine);
        _flashCoroutine = null;

        // reset material
        ChangeMaterial(_defaultMaterial);
        _activeGlowMatIndex = 0;
    }

    private IEnumerator FlashCoroutine()
    {
        while (true)
        {
            var newMat = _glowMats[_activeGlowMatIndex];
            ChangeMaterial(newMat);

            _activeGlowMatIndex += 1;
            if (_activeGlowMatIndex >= _glowMats.Count)
                _activeGlowMatIndex = 0;

            yield return new WaitForSeconds(_transitionDuration);
        }
    }

    private void ChangeMaterial(Material mat)
    {
        var newMats = new[] { mat };
        _meshRenderer.materials = newMats;

        // handle child component
        foreach (var mr in _childrenMeshRenderers)
        {
            mr.sharedMaterials = newMats;
        }
    }

    private void GetMeshRenderers(GameObject owner, out MeshRenderer meshRenderer,
        out List<MeshRenderer> childrenMeshRenderers)
    {
        meshRenderer = owner.GetComponent<MeshRenderer>();

        childrenMeshRenderers = new List<MeshRenderer>();
        foreach (Transform child in owner.transform)
        {
            var mr = child.gameObject.GetComponent<MeshRenderer>();
            if (mr != null)
                childrenMeshRenderers.Add(mr);
        }
    }
}