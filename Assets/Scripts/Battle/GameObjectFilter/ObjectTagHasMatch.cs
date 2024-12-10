
using Sirenix.OdinInspector;
using UnityEngine;

[LabelText("对象Tag是否匹配")]
public class ObjectTagHasMatch : ObjectFilter
{
    [LabelText("目标Tag"), ValueDropdown("SelectTags")]
    public string Tag;
    public override bool HasMatch(GameObject gameObject)
    {
        return gameObject.CompareTag(Tag);
    }
    
    private string[] SelectTags()
    {
        return UnityEditorInternal.InternalEditorUtility.tags;
    }
}