using UnityEngine;

public class NPCIdleState : NPCLocomotionBaseState
{
    public NPCIdleState(FollowAndArcMovement context) : base(context)
    {
    }

    public override void OnEnterState()
    {
        Debug.Log("NPC Enter Idle State");
    }

    public override void OnExitState()
    {
        Debug.Log("NPC Exit Idle State");
    }
}