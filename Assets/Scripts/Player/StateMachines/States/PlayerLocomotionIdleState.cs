using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class PlayerLocomotionIdleState : PlayerBaseState
{
    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Play Animation")]
    private PlayerLocomotionAnimationCommand _playAnimationCommand;

    private void OnEnable()
    {
        _playAnimationCommand.Execute(this);
    }
}