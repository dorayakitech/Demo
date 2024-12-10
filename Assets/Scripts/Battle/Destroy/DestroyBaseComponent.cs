using Sirenix.OdinInspector;
using UnityEngine;

public abstract class DestroyBaseComponent : SerializedMonoBehaviour
{
    protected void DestroyHandle()
    {
        var allDisplayComps = gameObject.GetComponentsInChildren<DisplayWhenDestroy>();
        foreach (var displayComp in allDisplayComps)
        {
            displayComp.Display();
        }
        Destroy(gameObject);
    }
}