using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(RespawnManager))]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Player Death")]
    private SOEvent _deathEvent;

    private BoxCollider _collider;
    private RespawnManager _respawnManager;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;

        _respawnManager = GetComponent<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(VariableNamesDefine.PlayerTag)) return;

        _deathEvent.Notify();
        _respawnManager.Respawn();
    }
}