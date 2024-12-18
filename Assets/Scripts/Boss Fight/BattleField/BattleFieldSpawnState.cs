using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BattleFieldSpawnState : BattleFieldBaseState
{
    public Action Callback;
    private EnergyLaserTurret _turret;

    public BattleFieldSpawnState(BattleField context) : base(context)
    {
    }

    public override void OnEnterState()
    {
        var turretObj = GenerateATurret();
        _turret = turretObj.GetComponent<EnergyLaserTurret>();

        // Show turret
        _turret.Show();

        // Start Countdown
        context.StartCoroutine(TurretLifeTimeCountdown(context.TurretExistDuration));
    }

    private GameObject GenerateATurret()
    {
        var turretPrefab = PickARandomElement(context.BossFightCfg.TurretsRandomPool);
        var spawnPoint = PickARandomElement(context.SpawnPoints);
        return Object.Instantiate(turretPrefab, spawnPoint.position, turretPrefab.transform.rotation);
    }

    private T PickARandomElement<T>(List<T> items)
    {
        var index = Random.Range(0, items.Count);
        return items[index];
    }

    private IEnumerator TurretLifeTimeCountdown(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        _turret.Hide(false, () => { Debug.Log("彻底隐藏了~~~"); });
    }
}