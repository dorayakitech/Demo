using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class RotateToTarget : Action
{
    public SharedGameObject Target;
    public float RotateSpeed = 1;
    public float MaxTime = 5;
    private float timer;
    //允许的误差
    public float AllowOffset = 6; 
    public override void OnStart()
    {
        timer = MaxTime;
    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }
        //忽略y轴
        //旋转到目标方向
        var targetPosition = Target.Value.transform.position;
        var direction = targetPosition - transform.position;
        direction.y = 0;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        //判断是否旋转到目标方向
        if (Quaternion.Angle(transform.rotation, targetRotation) < AllowOffset || timer <= 0)
        {
            return TaskStatus.Success;
        }
        timer -= Time.deltaTime;
        return TaskStatus.Running;
        
    }
}