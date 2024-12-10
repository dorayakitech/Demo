using Sirenix.OdinInspector;
using UnityEngine;

public class SendEventWhenBeTrigger : BeTriggerDisplayBase
{
    [Required]
    public SOEvent Event;
    protected override void DisplayHandle(Vector3 touchPoint)
    {
        if(Event != null)
            Event.Notify();
    }
}