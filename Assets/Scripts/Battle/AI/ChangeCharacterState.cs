using Animancer;
using Animancer.FSM;
using BehaviorDesigner.Runtime.Tasks;

public class ChangeCharacterState : Action
{
    private AnimancerComponent _animancer;
    public TransitionAsset TargetState;
    public override void OnStart()
    {
        var animancerOwner = gameObject.GetComponent<IAnimancerOwner>();
        if(animancerOwner != null)
        {
            _animancer = animancerOwner.Animancer;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (_animancer != null && TargetState != null)
        {
            _animancer.Play(TargetState);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}