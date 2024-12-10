using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ObjectFilterType
{
    AllPass,
    AnyPass,
}

//配置多个筛选器，配置筛选类型:全部通过，任意通过
[LabelText("对象筛选器组")]
public class ObjectFilterGroup : ObjectFilter
{
    public ObjectFilterType FilterType;
    public List<ObjectFilter> Filters;
    public override bool HasMatch(GameObject gameObject)
    {
        if (Filters == null || Filters.Count == 0)
            return true;
        
        if(FilterType == ObjectFilterType.AllPass)
        {
            foreach (ObjectFilter filter in Filters)
            {
                if (!filter.HasMatch(gameObject))
                    return false;
            }
            return true;
        }
        else
        {
            foreach (ObjectFilter filter in Filters)
            {
                if (filter.HasMatch(gameObject))
                    return true;
            }
            return false;
        }
    }
}