using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
//攻击蓄力任务
public class ChargeAttackTask : Action
{
    //蓄力时间
    public SharedFloat chargeTime = 1;
    //计时器
    private float timer;
    
    public override void OnStart()
    {
        timer = 0;
        Debug.Log("开始蓄力");
    }
    
    public override TaskStatus OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= chargeTime.Value)
        {
            Debug.Log("蓄力完成");
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        Debug.Log("结束蓄力");
    }
}