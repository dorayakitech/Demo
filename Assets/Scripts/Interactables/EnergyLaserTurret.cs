using UnityEngine;

public class EnergyLaserTurret : EnergyBallReceiver
{
    private Transform _chargingPoint;
    private Transform _shootingPoint;

    public Transform ChargingPoint => _chargingPoint;
    public Transform ShootingPoint => _shootingPoint;

    protected override void Awake()
    {
        base.Awake();
        _chargingPoint = transform.Find(VariableNamesDefine.LaserTurretChargingPoint);
        _shootingPoint = transform.Find(VariableNamesDefine.LaserTurretShootingPoint);
    }
}