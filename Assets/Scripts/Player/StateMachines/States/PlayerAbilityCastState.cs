using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAbilityCastState : PlayerBaseState
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _animation;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private StringAsset _timeToDisappearInteractiveSphere;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Animation End")]
    private SOEvent _animationEndEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Interactive Sphere Disappear")]
    private SOEvent _interactiveSphereDisappearEvent;

    private void OnEnable()
    {
        var state = Player.Instance.AbilityLayer.Play(_animation);
        state.Events(this).OnEnd ??= () => { _animationEndEvent.Notify(); };
        state.Events(this).SetCallback(_timeToDisappearInteractiveSphere.Name,
            () => { _interactiveSphereDisappearEvent.Notify(); });
    }

    private void OnDisable()
    {
        Player.Instance.AbilityLayer.StartFade(0.0f);
    }
}