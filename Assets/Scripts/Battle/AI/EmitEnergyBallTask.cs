using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

//发射能量球任务
public class EmitEnergyBallTask : Action
{
    public GameObject energyBallPrefab;
    public string FirePointPath;
    public SharedVector3 TargetPosition;

    public float OffsetY = 0f;
    public float TargetOffsetY = 0f;

    //todo:如果做得细一点，实际上这里还需要一个Dir和FirePoint
    public override TaskStatus OnUpdate()
    {
        EmitEnergyBall();

        return TaskStatus.Success;
    }

    private void EmitEnergyBall()
    {
        Debug.Log("发射能量球");
        if (energyBallPrefab != null)
        {
            var ball = GameObject.Instantiate(energyBallPrefab);
            var moveComp = ball.GetComponent<IMoveAble>();
            if (moveComp != null)
            {
                Vector3 targetPosition;
                if (FirePointPath != null)
                {
                    var firePoint = transform.Find(FirePointPath);
                    if (firePoint != null)
                    {
                        ball.transform.position = firePoint.position;
                        MoveBall(moveComp, firePoint.position);
                    }
                    else
                        Debug.LogError($"未找到路径：{FirePointPath}");
                }
                else
                {
                    var pos = transform.position;
                    pos.y += OffsetY;
                    ball.transform.position = pos;
                    MoveBall(moveComp, pos);
                }
            }
        }
    }
    
    void MoveBall(IMoveAble moveComp, Vector3 firePos)
    {
        var targetPosition = TargetPosition.Value;
        targetPosition.y += TargetOffsetY;
        var dir = (targetPosition - firePos).normalized;
        //终点
        var terminalPos = firePos + dir * 100;
        moveComp.MoveToPosition(terminalPos);
    }
}