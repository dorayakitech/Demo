using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField, Required] private int _lockNum;

    [SerializeField, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Activate")]
    private SOOperateLockEvent _activateEvent;

    [SerializeField, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Deactivate")]
    private SOOperateLockEvent _deactivateEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _idle;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _open;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _close;

    private AnimancerComponent _animancer;

    private void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
        _animancer.Play(_idle);
    }

    private void OnEnable()
    {
        _activateEvent?.Subscribe(Activate);
        _deactivateEvent?.Subscribe(Deactivate);
    }

    private void OnDisable()
    {
        _activateEvent?.Unsubscribe(Activate);
        _deactivateEvent?.Unsubscribe(Deactivate);
    }

    private void Activate(int lockNum)
    {
        _animancer.Play(_open);
    }

    private void Deactivate(int lockNum)
    {
        _animancer.Play(_close);
    }
}