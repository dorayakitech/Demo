using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class PlayerAbilityChargeState : PlayerBaseState
{
    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Play Animation")]
    private PlayerAbilityPlayChargeAnimationCommand _playAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Fade Out Animation")]
    private PlayerAbilityFadeOutAnimationCommand _fadeOutAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Appear Interactive Sphere")]
    private InteractiveSphereAppearCommand _interactiveSphereAppearCommand;

    private void OnEnable()
    {
        _playAnimationCommand.Execute(this);
        _interactiveSphereAppearCommand.Execute(this);
    }

    private void OnDisable()
    {
        _fadeOutAnimationCommand.Execute(this);
    }
}