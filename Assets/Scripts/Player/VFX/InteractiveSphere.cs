using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphere : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] private SOEnergyBallInteractiveSphereConfig _config;

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
        if (!GlobalVariablesManager.Instance.GetValue(GlobalVariableNamesDefine.PlayerLeftHand, out _playerLeftHand))
        {
            Debug.LogError("Player Left Hand not found");
        }

        transform.DOKill();
        transform.DOScale(Vector3.one * _config.Range, _config.AppearDuration);
    }

    private void LateUpdate()
    {
        transform.position = _playerLeftHand.transform.TransformPoint(Vector3.zero);
    }

    private void OnDisappearEvent()
    {
        transform.DOScale(Vector3.zero, _config.AppearDuration).OnComplete(() => { Destroy(gameObject); });
    }

    private void OnCancelEvent()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, _config.CancelDuration).OnComplete(() => { Destroy(gameObject); });
    }
}