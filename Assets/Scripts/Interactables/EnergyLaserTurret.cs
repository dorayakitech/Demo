using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnergyLaserTurret : EnergyBallReceiver, IHidden
{
    [SerializeField, Required] private bool _initShow;
    [SerializeField, Required] private float _transitionDuration = 1.0f;
    [SerializeField] private List<ICommand> _tasksAfterShow = new();

    private Transform _chargingPoint;
    private Transform _shootingPoint;
    private Transform _gun;
    private List<Collider> _colliders = new();

    public Transform ChargingPoint => _chargingPoint;
    public Transform ShootingPoint => _shootingPoint;
    public Transform Gun => _gun;

    public bool CurrentShow { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _chargingPoint = transform.Find(VariableNamesDefine.LaserTurretChargingPoint);
        _shootingPoint = transform.Find(VariableNamesDefine.LaserTurretShootingPoint);
        _gun = transform.Find(VariableNamesDefine.LaserTurretGun);
        _colliders = GetComponentsInChildren<Collider>().ToList();

        if (!_initShow)
            Hide();
    }

    public void Show()
    {
        CurrentShow = true;

        foreach (var mr in meshRenderers)
        {
            mr.enabled = true;
        }

        foreach (var mat in currentMaterials)
        {
            mat.DOFade(1.0f, _transitionDuration).OnComplete(ExecuteTasksAfterShow);
        }

        foreach (var c in _colliders)
        {
            c.enabled = true;
        }
    }

    private void Hide()
    {
        CurrentShow = false;

        foreach (var mr in meshRenderers)
        {
            mr.enabled = false;
        }

        foreach (var mat in currentMaterials)
        {
            mat.DOFade(0.0f, 0.0f);
        }

        foreach (var c in _colliders)
        {
            c.enabled = false;
        }
    }

    private void ExecuteTasksAfterShow()
    {
        foreach (var task in _tasksAfterShow)
        {
            task.Execute(this);
        }
    }
}