﻿using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathPanel : MonoBehaviour
{
    [SerializeField, Required] private float _transitionDuration = 0.25f;
    [SerializeField, Required] private float _delayBetweenTransition = 0.25f;

    [HideInInspector] public Action Callback;

    private Image _image;
    private Coroutine _transitionCoroutine;
    private Tweener _tweener;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (_transitionCoroutine != null)
            StopCoroutine(_transitionCoroutine);

        _transitionCoroutine = StartCoroutine(StartFadeInAndOut());
    }

    private IEnumerator StartFadeInAndOut()
    {
        _tweener?.Kill();

        // fade in firstly
        _tweener = _image.DOFade(1.0f, _transitionDuration);
        yield return _tweener.WaitForCompletion();

        // wait
        yield return new WaitForSeconds(_delayBetweenTransition);

        // fade out
        _tweener = _image.DOFade(0.0f, _transitionDuration);
        yield return _tweener.WaitForCompletion();

        Callback?.Invoke();
        _transitionCoroutine = null;
    }

    public void SetEnable(bool enable)
    {
        gameObject.SetActive(enable);
    }
}