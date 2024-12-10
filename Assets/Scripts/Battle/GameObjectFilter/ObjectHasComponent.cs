using System;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

[LabelText("对象是否有某个组件")]
public class ObjectHasComponent : ObjectFilter
{
    // [TypeFilter("SelectComponentType")]
    [ValueDropdown ("SelectComponentType")]
    public Type ComponentType;
    public override bool HasMatch(GameObject gameObject)
    {
        return gameObject.GetComponent(ComponentType) != null;
    }
    
    private Type[] SelectComponentType()
    {
        return Assembly.GetExecutingAssembly().GetTypes().Where(FilterDerivedTypes).ToArray();
    }
    
    private bool FilterDerivedTypes(Type t)
    {
        //继承自MonoBehaviour且不是抽象类
        return t.IsSubclassOf(typeof(MonoBehaviour)) && !t.IsAbstract;
    }
}