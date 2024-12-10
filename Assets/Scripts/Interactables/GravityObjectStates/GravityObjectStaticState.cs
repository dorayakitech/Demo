using UnityEngine;

public class GravityObjectStaticState : GravityObjectBaseState
{
    public GravityObjectStaticState(GravityObject context) : base(context)
    {
    }

    public override void OnEnterState()
    {
        Debug.Log("Enter Static State");
        context.IgnoreCollisionBetweenPlayer(false);
    }

    public override void OnExitState()
    {
        Debug.Log("Exit Static State");
    }
}