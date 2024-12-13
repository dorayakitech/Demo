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
    private TransitionAsset _open;

    private AnimancerComponent _animancer;
    private AnimancerState _gateAnimationState;

    private void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
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

    private void Activate(int activeLockNum)
    {
        if (activeLockNum != _lockNum) return;

        _gateAnimationState = _animancer.Play(_open);

        _gateAnimationState.Speed = 1.0f;
        _gateAnimationState.IsPlaying = true;
        _gateAnimationState.Events(this).OnEnd ??= () => { _gateAnimationState.IsPlaying = false; };
    }

    private void Deactivate(int deactiveLockNum)
    {
        if (deactiveLockNum != _lockNum) return;

        _gateAnimationState = _animancer.Play(_open);
        _gateAnimationState.IsPlaying = true;
        _gateAnimationState.Speed = -1.0f;
    }
}