using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyBallInteractiveSphereConfig",
    menuName = "Scriptable Object/Configs/Energy Ball Interactive Sphere")]
public class SOEnergyBallInteractiveSphereConfig : ScriptableObject
{
    [Required] public GameObject Prefab;
    [Required] public float Range;
    [Required] public float AppearDuration;
    [Required] public float CancelDuration;
    [Required] public float DisappearDuration;
}