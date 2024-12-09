using Sirenix.OdinInspector;
using UnityEngine;

public class GravityObjectTouchGroundTrigger : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Touch Ground")]
    private SOGameObjectNotifiedEvent _touchGroundEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(VariableNamesDefine.GroundTag)) return;

        _touchGroundEvent.Notify(transform.parent.gameObject);
    }
}