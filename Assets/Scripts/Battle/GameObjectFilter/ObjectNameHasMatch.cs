
using Sirenix.OdinInspector;
using UnityEngine;

[LabelText("对象名是否匹配")]
public class ObjectNameHasMatch : ObjectFilter
{ 
    [LabelText("目标对象名")]
    public string TargetName;
    public override bool HasMatch(GameObject gameObject)
    {
        return gameObject.name == TargetName;
    }
}