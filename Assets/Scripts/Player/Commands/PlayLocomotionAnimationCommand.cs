using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayLocomotionAnimationCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private TransitionAsset _animation;

    public void Execute<T>(T receiver)
    {
        Player.Instance.LocomotionLayer.Play(_animation);
    }
}