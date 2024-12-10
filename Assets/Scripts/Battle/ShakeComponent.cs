using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShakeComponent : MonoBehaviour
{
    [LabelText("震动事件"), Required]
    public SOEvent ShakeEvent;
    public float Duration = 0.5f;
    public float Strength = 1f;

    private void OnEnable()
    {
        ShakeEvent.Subscribe(Shake);
    }

    Tweener _tweener;
    private void Shake()
    {
        if (_tweener != null)
        {
            _tweener.Complete(true);
            _tweener = null;
        }
        //震动，震动结束后不应该保持在震动前的位置
        var oldPos = transform.position;
        _tweener = transform.DOShakePosition(Duration, Strength).OnComplete(() =>
        {
            transform.position = oldPos;
            _tweener = null;
        });
    }
    
    private void OnDisable()
    {
        ShakeEvent.Unsubscribe(Shake);
    }
}