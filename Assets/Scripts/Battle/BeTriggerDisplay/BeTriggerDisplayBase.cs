using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 被碰到后的表现脚本
/// 发事件、播放特效、播放打击音效、子弹弹坑贴花等
/// </summary>
public abstract class BeTriggerDisplayBase : SerializedMonoBehaviour
{
    [Required]
    public ObjectFilter Filter;
    private void OnTriggerEnter(Collider other)
    {
        if (Filter == null)
            return;
        bool isMatch = Filter.HasMatch(other.gameObject);

        if (isMatch)
        {
            //触碰点
            var point = other.ClosestPointOnBounds(transform.position);
            DisplayHandle(point);
        }
    }

    protected virtual void DisplayHandle(Vector3 touchPoint)
    {
    }
}