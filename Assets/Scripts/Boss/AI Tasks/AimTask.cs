using Animancer;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Custom"), TaskName("Boss Aim")]
public class AimTask : Action
{
    [SerializeField] private SOBossFightConfig _bossFightConfig;
    [SerializeField] private float _rotateSpeed;

    private AnimancerComponent _animancer;
    private Rigidbody _rb;
    private GameObject _weapon;

    public override void OnAwake()
    {
        _rb = GetComponent<Rigidbody>();
        _animancer = GetComponent<AnimancerComponent>();

        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.BossWeaponObject, out _weapon))
            Debug.LogError("Boss Weapon Not Found");
    }

    public override void OnStart()
    {
        var state = _animancer.Play(_bossFightConfig.AimAnimation);
        state.Events(this).OnEnd ??= () => { state.IsPlaying = false; };
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }

    public override void OnFixedUpdate()
    {
        Aim();
    }

    private void Aim()
    {
        var targetDir = Player.Instance.transform.position - _weapon.transform.position;
        targetDir.y = 0.0f;
        targetDir.Normalize();

        var diffRot = Quaternion.FromToRotation(_weapon.transform.forward, targetDir);
        var targetRot = diffRot * transform.rotation;

        var rotThisFrame = Quaternion.Slerp(transform.rotation, targetRot, _rotateSpeed * Time.deltaTime);
        _rb.MoveRotation(rotThisFrame);
    }
}