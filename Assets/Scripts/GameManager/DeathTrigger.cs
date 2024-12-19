using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(RespawnManager))]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Player Death")]
    private SOPlayerDeathEvent _deathEvent;

    [SerializeField, Required] private int _roomNum;

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

        _deathEvent.Notify(_roomNum);
        _respawnManager.Respawn();
    }
}