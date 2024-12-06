using Animancer;
using Animancer.FSM;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAbilityChargeState : PlayerBaseState
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _animation;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Animation End")]
    private SOEvent _animationEndEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Interactive Sphere Appear")]
    private SOEvent _interactiveSphereAppearEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Interactive Sphere Cancel")]
    private SOEvent _interactiveSphereCancelEvent;

    private void OnEnable()
    {
        var state = Player.Instance.AbilityLayer.Play(_animation);
        state.Events(this).OnEnd ??= () => { _animationEndEvent.Notify(); };

        _interactiveSphereAppearEvent.Notify();
    }

    private void OnDisable()
    {
        Player.Instance.AbilityLayer.StartFade(0.0f);

        if (this.GetNextState<PlayerBaseState>() is PlayerAbilityIdleState)
        {
            _interactiveSphereCancelEvent.Notify();
        }
    }
}