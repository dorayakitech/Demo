using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    public void Execute<T>(T receiver)
    {
        var offset = Player.Instance.Animancer.Animator.deltaPosition;
        var currentPos = Player.Instance.Rb.position;
        Player.Instance.Rb.MovePosition(currentPos + offset);
    }
}

public class PlayerLookCommand : ICommand
{
    [SerializeField, Required] private float _rotateSpeed = 10.0f;

    public void Execute<T>(T receiver)
    {
        if (Player.Instance.InputValue.MoveVector == Vector2.zero) return;

        var targetDir = Player.Instance.InputValue.MoveVector;
        var lookDir = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.y).ToIsometric());
        var startRot = Player.Instance.Rb.rotation;
        var targetRot = Quaternion.Slerp(startRot, lookDir, _rotateSpeed * Time.deltaTime);
        Player.Instance.Rb.MoveRotation(targetRot);
    }
}