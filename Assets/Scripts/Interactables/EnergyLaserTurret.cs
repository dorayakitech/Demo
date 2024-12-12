using UnityEngine;

public class EnergyLaserTurret : EnergyBallReceiver
{
    private Transform _chargingPoint;
    private Transform _shootingPoint;
    private Transform _gun;

    public Transform ChargingPoint => _chargingPoint;
    public Transform ShootingPoint => _shootingPoint;
    public Transform Gun => _gun;

    protected override void Awake()
    {
        base.Awake();
        _chargingPoint = transform.Find(VariableNamesDefine.LaserTurretChargingPoint);
        _shootingPoint = transform.Find(VariableNamesDefine.LaserTurretShootingPoint);
        _gun = transform.Find(VariableNamesDefine.LaserTurretGun);
    }
}