using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShootEnergyLaserCommand : ICommand
{
    [SerializeField, Required] private float _chargingDuration = 2.0f;
    [SerializeField, Required, AssetsOnly] private GameObject _chargingVFX;
    [SerializeField, Required, AssetsOnly] private GameObject _laser;

    private EnergyLaserTurret _turret;
    private bool _isInProgress;

    public void Execute<T>(T receiver)
    {
        if (receiver is not EnergyLaserTurret turret) return;
        _turret = turret;

        if (_isInProgress) return;
        _isInProgress = true;
        _turret.StartCoroutine(ChargeForShoot(_chargingDuration));
    }

    private IEnumerator ChargeForShoot(float duration)
    {
        var chargingVFX = Object.Instantiate(_chargingVFX,
            _turret.ChargingPoint.position, Quaternion.identity, _turret.transform);
        Object.Destroy(chargingVFX, duration);

        yield return new WaitForSeconds(duration);

        Object.Instantiate(_laser, _turret.ShootingPoint.position,
            _turret.ShootingPoint.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f));
        _isInProgress = false;
    }
}