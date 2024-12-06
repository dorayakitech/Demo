using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

public class VFXEffect : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Animations"), LabelText("appear")]
    private TransitionAsset _appearAnimation;

    private AnimancerComponent _animancer;

    private void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
    }

    private void Start()
    {
        _animancer.Play(_appearAnimation);
    }
}