using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAbilityPlayChargeAnimationCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private TransitionAsset _animation;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Animation End")]
    private SOEvent _animationEndEvent;

    public virtual void Execute<T>(T receiver)
    {
        var state = Player.Instance.AbilityLayer.Play(_animation);
        state.Events(this).OnEnd ??= () => { _animationEndEvent.Notify(); };
    }
}

public class PlayerAbilityPlayCastAnimationCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private TransitionAsset _animation;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Animation End")]
    private SOEvent _animationEndEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Cast Midway Event")]
    private SOEvent _animationEvent;

    private static readonly StringReference _animationEventName = "TimeToDisappearInteractiveSphere";

    public virtual void Execute<T>(T receiver)
    {
        var state = Player.Instance.AbilityLayer.Play(_animation);
        state.Events(this).OnEnd ??= () => { _animationEndEvent.Notify(); };
        state.Events(this).SetCallback(_animationEventName, () => { _animationEvent.Notify(); });
    }
}

public class PlayerAbilityFadeOutAnimationCommand : ICommand
{
    public void Execute<T>(T receiver)
    {
        Player.Instance.AbilityLayer.StartFade(0.0f);
    }
}