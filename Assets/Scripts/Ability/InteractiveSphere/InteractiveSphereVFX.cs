using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphereVFX : MonoBehaviour
{
    [SerializeField, Required, BoxGroup("Properties")]
    private float _range;

    [SerializeField, Required, BoxGroup("Properties")]
    private float _appearDuration;

    [SerializeField, Required, BoxGroup("Properties")]
    private float _cancelDuration;

    [SerializeField, Required, BoxGroup("Properties")]
    private float _disappearDuration;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed")]
    private SOEvent _disappearEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed")]
    private SOEvent _cancelEvent;

    private GameObject _playerLeftHand;

    private void OnEnable()
    {
        _disappearEvent.Subscribe(OnDisappearEvent);
        _cancelEvent.Subscribe(OnCancelEvent);
    }

    private void OnDisable()
    {
        _disappearEvent.Unsubscribe(OnDisappearEvent);
        _cancelEvent.Unsubscribe(OnCancelEvent);
    }

    private void Start()
    {
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerLeftHand, out _playerLeftHand))
        {
            Debug.LogError("Player Left Hand not found");
        }

        transform.DOKill();
        transform.DOScale(Vector3.one * _range, _appearDuration);
    }

    private void LateUpdate()
    {
        transform.position = _playerLeftHand.transform.TransformPoint(Vector3.zero);
    }

    private void OnDisappearEvent()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, _disappearDuration).OnComplete(() => { Destroy(gameObject); });
    }

    private void OnCancelEvent()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, _cancelDuration).OnComplete(() => { Destroy(gameObject); });
    }
}