using UnityEngine;

public class GravityObjectStaticState : GravityObjectBaseState
{
    private void OnEnable()
    {
        Debug.Log($"{stateMachine.MainObj.name} Enter Static State");
        stateMachine.IgnoreCollisionBetweenPlayer(false);
    }
}