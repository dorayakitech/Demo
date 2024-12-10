
using UnityEngine;

/// <summary>
/// 判断某个GameObject是否符合条件
/// </summary>
public abstract class ObjectFilter
{
    public abstract bool HasMatch(GameObject gameObject);
}

public class EmptyFilter : ObjectFilter
{
    public override bool HasMatch(GameObject gameObject)
    {
        return true;
    }
}