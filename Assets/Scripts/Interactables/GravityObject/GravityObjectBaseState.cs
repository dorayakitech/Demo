using Animancer.FSM;

public abstract class GravityObjectBaseState : StateBehaviour
{
    protected GravityObjectStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<GravityObjectStateMachine>();
    }
}