using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField, Required] private float _floatingDistance = 0.5f;
    [SerializeField, Required] private float _floatingDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(WaitForRandomSeconds());
    }

    private IEnumerator WaitForRandomSeconds()
    {
        var startTime = Random.Range(0.0f, _floatingDuration * 1.5f);
        yield return new WaitForSeconds(startTime);
        StartFloating();
    }

    private void StartFloating()
    {
        transform.DOMoveY(_floatingDistance, _floatingDuration)
            .SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}