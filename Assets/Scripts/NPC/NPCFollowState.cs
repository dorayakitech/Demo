using DG.Tweening;
using UnityEngine;

public class NPCFollowState : NPCLocomotionBaseState
{
    public NPCFollowState(FollowAndArcMovement context) : base(context)
    {
    }

    private Tweener _tweener;

    public override void OnEnterState()
    {
        Debug.Log("NPC Enter Follow State");
        _tweener = context.transform.DOMove(context.FollowStartPosition, context.FollowDuration);
    }

    public override void LateUpdate()
    {
        _tweener.ChangeValues(context.transform.position, context.FollowStartPosition);
    }

    public override void OnExitState()
    {
        Debug.Log("NPC Exit Follow State");
        _tweener.Kill();
        _tweener = null;
    }
}