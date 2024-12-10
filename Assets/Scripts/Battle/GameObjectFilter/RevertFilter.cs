
using Sirenix.OdinInspector;
using UnityEngine;

[LabelText("反转筛选器")]
public class RevertFilter : ObjectFilter
{
    public ObjectFilter Filter;
    public override bool HasMatch(GameObject gameObject)
    {
        return !Filter.HasMatch(gameObject);
    }
}