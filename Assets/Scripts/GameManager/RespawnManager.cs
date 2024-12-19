using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RespawnManager : SerializedMonoBehaviour
{
    [SerializeField, Required, SceneObjectsOnly]
    private Transform _respawnPoint;

    [SerializeField, Required] private List<ICommand> _respawnProcess = new();
    [SerializeField, Required] private float _timeBeforeRespawn = 1.0f;

    private Coroutine _respawnCoroutine;

    public void Respawn()
    {
        if (_respawnCoroutine != null)
            StopCoroutine(_respawnCoroutine);

        _respawnCoroutine = StartCoroutine(WaitForRespawn());
    }

    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(_timeBeforeRespawn);
        Player.Instance.transform.position = _respawnPoint.position;
        Player.Instance.transform.rotation = Quaternion.identity;

        foreach (var task in _respawnProcess)
        {
            task.Execute(this);
        }
    }
}