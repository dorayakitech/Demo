using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GravityObjectElevateState : GravityObjectBaseState
{
    [SerializeField, Required] [InfoBox("Distance between this object and player left hand")]
    private Vector3 _playerLeftHandOffset;

    [SerializeField, Required] private float _elevateDuration = 1.0f;

    private Tweener _tweener;

    private Vector3 MoveEndPoint
    {
        get
        {
            var handForward = -stateMachine.PlayerLeftHand.transform.right;
            handForward.y = 0.0f;
            handForward.Normalize();
            return stateMachine.PlayerLeftHand.transform.position + handForward * _playerLeftHandOffset.z +
                   Vector3.up * _playerLeftHandOffset.y;
        }
    }

    private void OnEnable()
    {
        Debug.Log($"{stateMachine.MainObj.name} Enter Elevate State");
        stateMachine.IgnoreCollisionBetweenPlayer(true);
    }

    private void OnDisable()
    {
        _tweener?.Kill();
        _tweener = null;
    }

    private void Update()
    {
        if (_tweener == null)
            _tweener = stateMachine.MainObj.transform.DOMove(MoveEndPoint, _elevateDuration);
        else
            _tweener.ChangeEndValue(MoveEndPoint, true);
    }
}