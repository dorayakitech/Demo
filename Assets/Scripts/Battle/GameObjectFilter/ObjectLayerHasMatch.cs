using Sirenix.OdinInspector;
using UnityEngine;

[LabelText("对象Layer是否匹配")]
public class ObjectLayerHasMatch : ObjectFilter
{
    [LabelText("目标Layer")]
    public LayerMask LayerMask;
    public override bool HasMatch(GameObject gameObject)
    {
        
        return (LayerMask & (1 << gameObject.layer)) != 0;
    }
}