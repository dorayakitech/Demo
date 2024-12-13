using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    [SerializeField, Required] private float _targetTransparency = 0.5f;
    [SerializeField, Required] private float _fadeDuration = 0.5f;
    private MeshRenderer _meshRenderer;
    private Material _material;
    private Tweener _tweener;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.materials[0];
    }

    public void FadeIn()
    {
        _tweener?.Kill();
        _tweener = _material.DOFade(_targetTransparency, _fadeDuration);
    }

    public void FadeOut()
    {
        _tweener?.Kill();
        _tweener = _material.DOFade(1.0f, _fadeDuration);
    }
}