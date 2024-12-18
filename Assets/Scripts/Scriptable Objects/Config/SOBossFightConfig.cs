using Animancer;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SOBossFightConfig", menuName = "Scriptable Object/Configs/Boss Fight")]
public class SOBossFightConfig : ScriptableObject
{
    [SerializeField, Required, AssetsOnly] public TransitionAsset IdleStateAnimation;
    [SerializeField, Required, AssetsOnly] public TransitionAsset AimAnimation;
    [SerializeField, Required, AssetsOnly] public TransitionAsset ShootAnimation;
    [SerializeField, Required, AssetsOnly] public TransitionAsset BeHitAnimation;
    [SerializeField, Required, AssetsOnly] public TransitionAsset StandUpAnimation;
    [SerializeField, Required, AssetsOnly] public TransitionAsset DefeatedAnimation;

    [SerializeField, Required, AssetsOnly] public ExternalBehaviorTree BossStage1AI;
}