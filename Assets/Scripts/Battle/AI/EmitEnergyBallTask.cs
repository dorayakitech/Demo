using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

//发射能量球任务
public class EmitEnergyBallTask : Action
{
    public GameObject energyBallPrefab;

    public float OffsetY = 0f;
    //todo:如果做得细一点，实际上这里还需要一个Dir和FirePoint
    public override TaskStatus OnUpdate()
    {
        EmitEnergyBall();
 
        return TaskStatus.Success;
    }
 
    private void EmitEnergyBall()
    {
        Debug.Log("发射能量球");
        if(energyBallPrefab != null)
        {
            var ball = GameObject.Instantiate(energyBallPrefab);
            var moveComp = ball.GetComponent<IMoveAble>();
            if (moveComp != null)
            {
                var pos = transform.position;
                pos.y += OffsetY;
                ball.transform.position = pos;
                moveComp.MoveToPosition(pos + transform.forward * 100);
            }
        }
    }
}