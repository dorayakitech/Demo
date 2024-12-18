using System.Collections.Generic;
using Animancer.FSM;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] private SOBossFightConfig _bossFightCfg;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Hit By Boss Laser")]
    private SOEvent _hitByBossLaserEvent;

    [SerializeField, Required, SceneObjectsOnly]
    private List<Transform> _spawnPoints = new();

    [SerializeField, Required] private float _turretExistDuration = 10.0f;

    private StateMachine<BattleFieldBaseState>.WithDefault _stateMachine = new();
    private BattleFieldBaseState _idle;
    private BattleFieldBaseState _spawn;

    public SOBossFightConfig BossFightCfg => _bossFightCfg;
    public float TurretExistDuration => _turretExistDuration;
    public List<Transform> SpawnPoints => _spawnPoints;

    private void Awake()
    {
        InitStates();
    }

    private void OnEnable()
    {
        _hitByBossLaserEvent.Subscribe(OnHitByBossLaser);
    }

    private void OnDisable()
    {
        _hitByBossLaserEvent.Unsubscribe(OnHitByBossLaser);
    }

    private void Start()
    {
        _stateMachine.DefaultState = _idle;
    }

    private void InitStates()
    {
        _idle = new BattleFieldIdleState(this);
        _spawn = new BattleFieldSpawnState(this);
    }

    private void OnHitByBossLaser()
    {
        _stateMachine.TrySetState(_spawn);
    }
}