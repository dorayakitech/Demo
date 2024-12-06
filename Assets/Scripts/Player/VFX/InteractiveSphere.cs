using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphere : MonoBehaviour
{
    [SerializeField, Required] [BoxGroup("Properties")]
    private float _targetRange;

    [SerializeField, Required] [BoxGroup("Properties")]
    private float _appearDuration = 1.0f;

    [SerializeField, Required] [BoxGroup("Properties")]
    private float _cancelDuration = 1.0f;

    [SerializeField, Required] [BoxGroup("Properties")]
    private float _disappearDuration = 1.0f;

    [SerializeField, Required] [BoxGroup("Properties")]
    private Transform _hand;

    [SerializeField, Required] [BoxGroup("Properties")]
    private ParticleSystem _effect;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("appear")]
    private SOEvent _interactiveSphereAppearEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("cancel")]
    private SOEvent _interactiveSphereCancelEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("disappear")]
    private SOEvent _interactiveSphereDisappearEvent;

    private Tweener _tweener;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _interactiveSphereAppearEvent.Subscribe(OnAppearEvent);
        _interactiveSphereCancelEvent.Subscribe(OnCancelEvent);
        _interactiveSphereDisappearEvent.Subscribe(OnDisappearEvent);
    }

    private void OnDisable()
    {
        _interactiveSphereAppearEvent.Unsubscribe(OnAppearEvent);
        _interactiveSphereCancelEvent.Unsubscribe(OnCancelEvent);
        _interactiveSphereDisappearEvent.Unsubscribe(OnDisappearEvent);
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void LateUpdate()
    {
        transform.position = _hand.TransformPoint(Vector3.zero);
    }

    private void OnAppearEvent()
    {
        if (_particleSystem == null)
        {
            _particleSystem = Instantiate(_effect, Vector3.zero, Quaternion.identity);
            _particleSystem.transform.parent = transform;
            _particleSystem.transform.localScale = Vector3.one;
        }

        transform.DOKill();
        transform
            .DOScale(new Vector3(_targetRange, _targetRange, _targetRange), _appearDuration);
    }

    private void OnCancelEvent()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, _cancelDuration).OnComplete(() => { Destroy(_particleSystem.gameObject); });
    }

    private void OnDisappearEvent()
    {
        transform.DOScale(Vector3.zero, _disappearDuration)
            .OnComplete(() => { Destroy(_particleSystem.gameObject); });
    }
}