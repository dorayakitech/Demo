using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class RandomPickAPressedPlate : Action
{
    public SharedGameObject RandomStore;
    public override TaskStatus OnUpdate()
    {
        var objs = UnityEngine.GameObject.FindObjectsByType<PressedPlate>(FindObjectsSortMode.None);
        if (objs.Length == 0)
        {
            return TaskStatus.Failure;
        }
        var randomIndex = Random.Range(0, objs.Length);
        RandomStore.Value = objs[randomIndex].gameObject;
        return TaskStatus.Success;
    }
}