using System;
using UnityEngine;

public class LineMove : MonoBehaviour, IMoveAble
{
    public float MoveSpeed = 5f;
    public Transform Target;
    public Vector3? TargetPosition;
    public void MoveToTarget(Transform target)
    {
        Target = target;
    }

    public void MoveToPosition(Vector3 position)
    {
        TargetPosition = position;
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, MoveSpeed * Time.fixedDeltaTime);
        }
        else if (TargetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition.Value, MoveSpeed * Time.fixedDeltaTime);
        }
    }
}