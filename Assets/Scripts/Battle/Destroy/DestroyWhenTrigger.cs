using Sirenix.OdinInspector;
using UnityEngine;

//碰到对象后销毁
[AddComponentMenu("Destroy/DestroyWhenTrigger")]
public class DestroyWhenTrigger : DestroyBaseComponent
{
    [LabelText("碰到几个符合的对象后销毁自身")]
    public int MaxTriggerCount = 1;
    [Required]
    public ObjectFilter Filter;
    private int _triggerCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (Filter == null)
            return;
        bool isMatch = Filter.HasMatch(other.gameObject);

        if (isMatch)
        {
            _triggerCount++;
            if (_triggerCount >= MaxTriggerCount)
            {
                DestroyHandle();
            }
        }
    }
}