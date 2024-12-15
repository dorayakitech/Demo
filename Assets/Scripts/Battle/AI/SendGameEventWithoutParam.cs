using BehaviorDesigner.Runtime.Tasks;

public class SendGameEventWithoutParam : Action
{
    public SOEvent Event;
    
    public override TaskStatus OnUpdate()
    {
        if(Event != null)
            Event.Notify();
        return TaskStatus.Success;
    }
}