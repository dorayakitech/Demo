using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerLocomotionIdleState : PlayerBaseState
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _animation;

    private void OnEnable()
    {
        Player.Instance.LocomotionLayer.Play(_animation);
    }
}