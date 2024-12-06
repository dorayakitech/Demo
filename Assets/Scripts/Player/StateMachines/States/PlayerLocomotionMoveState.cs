using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class PlayerLocomotionMoveState : PlayerBaseState
{
    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Play Animation")]
    private PlayerLocomotionAnimationCommand _playAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Look")]
    private PlayerLookCommand _playerLookCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Move")]
    private PlayerMoveCommand _playerMoveCommand;

    private void OnEnable()
    {
        _playAnimationCommand.Execute(this);
    }

    private void Update()
    {
        _playerLookCommand.Execute(this);
    }

    private void OnAnimatorMove()
    {
        _playerMoveCommand.Execute(this);
    }
}