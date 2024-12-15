using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ForwardMove : Action
{
    //往前移动
    public float Speed = 1;
    public float MaxTime = 5;
    private float timer;
    public override void OnStart()
    {
        timer = MaxTime;
    }
    
    public override TaskStatus OnUpdate()
    {
        //往前移动
        transform.position += transform.forward * Speed * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}