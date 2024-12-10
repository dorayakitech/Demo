using Sirenix.OdinInspector;
using UnityEngine;

public class DisplayVFXWhenObjectBeDestroy : DisplayWhenDestroy
{
    [Required, AssetsOnly, LabelText("物体被销毁时展示的特效")]
    public GameObject VFX;
    public override void Display()
    {
        var instance = Instantiate(VFX, transform.position, transform.rotation);
        var particleSystem = instance.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(instance, particleSystem.totalTime + 0.1f);
        }
        else
            Destroy(instance, 2);
    }
}