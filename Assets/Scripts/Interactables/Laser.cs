using Sirenix.OdinInspector;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public enum Source
    {
        Turret,
        Boss
    }

    [SerializeField, Required] private float _flyForce = 100.0f;
    [SerializeField, Required, AssetsOnly] private GameObject _hitVFX;

    [SerializeField, Required] [BoxGroup("Collision"), LabelText("Ignore Player")]
    private bool _ignoreCollisionBetweenPlayer;

    [SerializeField, Required] [BoxGroup("Collision"), LabelText("Ignore Boss")]
    private bool _ignoreCollisionBetweenBoss;

    [SerializeField, Required, EnumToggleButtons]
    private Source _source = Source.Turret;

    private Rigidbody _rb;
    private Collider _collider;

    public Source LaserSource => _source;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        HandleCollision();
        Fly();
    }

    private void Fly()
    {
        _rb.AddForce(-transform.forward * _flyForce, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        var contactPoint = other.GetContact(0);
        Instantiate(_hitVFX, contactPoint.point, Quaternion.identity);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void HandleCollision()
    {
        if (_ignoreCollisionBetweenPlayer)
        {
            if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerCollider,
                    out CapsuleCollider playerCollider))
                Debug.LogError("Player Collider Not Found");

            Physics.IgnoreCollision(_collider, playerCollider, true);
        }

        if (_ignoreCollisionBetweenBoss)
        {
            if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.BossCollider,
                    out CapsuleCollider bossCollider))
                Debug.LogError("Player Collider Not Found");

            Physics.IgnoreCollision(_collider, bossCollider, true);
        }
    }
}