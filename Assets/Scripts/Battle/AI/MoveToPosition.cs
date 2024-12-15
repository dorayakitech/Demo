using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToPosition : Action
{
    public SharedFloat speed;
    public SharedVector3 targetPosition;

    public override TaskStatus OnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.Value, speed.Value * Time.deltaTime);
        //足够近？
        var sqrMagnitude = (transform.position - targetPosition.Value).sqrMagnitude;
        if (sqrMagnitude < 0.01f)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}