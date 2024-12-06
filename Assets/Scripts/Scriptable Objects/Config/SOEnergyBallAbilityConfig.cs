using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SOEnergyBallAbilityConfig", menuName = "Scriptable Object/Configs/Energy Ball Ability")]
public class SOEnergyBallAbilityConfig : ScriptableObject
{
    [Required] public GameObject InteractiveSpherePrefab;
    [Required] public GameObject EnergyBallPrefab;

    [Required] public float Range;
    [Required] public float AppearDuration;
    [Required] public float CancelDuration;
    [Required] public float DisappearDuration;
    [Required] public float FlyingForce;
}