using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerLocomotionMoveState : PlayerBaseState
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations")]
    private TransitionAsset _animation;

    [SerializeField, Required] [BoxGroup("Properties")]
    private float _rotateSpeed = 10.0f;

    private void OnEnable()
    {
        Player.Instance.LocomotionLayer.Play(_animation);
    }

    private void Update()
    {
        Look();
    }

    private void OnAnimatorMove()
    {
        Move();
    }

    private void Look()
    {
        if (Player.Instance.InputValue.MoveVector == Vector2.zero) return;

        var targetDir = Player.Instance.InputValue.MoveVector;
        var lookDir = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.y).ToIsometric());
        var startRot = Player.Instance.Rb.rotation;
        var targetRot = Quaternion.Slerp(startRot, lookDir, _rotateSpeed * Time.deltaTime);
        Player.Instance.Rb.MoveRotation(targetRot);
    }

    private void Move()
    {
        var offset = Player.Instance.Animancer.Animator.deltaPosition;
        var currentPos = Player.Instance.Rb.position;
        Player.Instance.Rb.MovePosition(currentPos + offset);
    }
}