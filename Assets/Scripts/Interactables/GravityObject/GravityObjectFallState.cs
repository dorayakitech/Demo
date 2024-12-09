using Sirenix.OdinInspector;
using UnityEngine;

public class GravityObjectFallState : GravityObjectBaseState
{
    private const float GravityForce = 9.8f;
    private float _freeFallingVelocity;
    private float _freeFallingHeight;

    private void OnEnable()
    {
        Debug.Log($"{stateMachine.MainObj.name} Enter Fall State");

        stateMachine.IgnoreCollisionBetweenPlayer(true);

        _freeFallingHeight = stateMachine.MainObj.transform.position.y;
        _freeFallingVelocity = 0.0f;
    }

    private void Update()
    {
        FreeFalling();
    }

    private void FreeFalling()
    {
        var deltaTime = Time.deltaTime;
        _freeFallingVelocity += GravityForce * deltaTime;
        _freeFallingHeight -= _freeFallingVelocity * deltaTime;
        stateMachine.MainObj.transform.position = new Vector3(stateMachine.MainObj.transform.position.x,
            _freeFallingHeight, stateMachine.MainObj.transform.position.z);
    }
}