using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField, Required] private float _flyForce = 100.0f;
    [SerializeField, Required, AssetsOnly] private GameObject _hitVFX;

    private Rigidbody _rb;
    private Collider _collider;
    private CapsuleCollider _playerCollider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerCollider, out _playerCollider))
            Debug.LogError("Player Collider Not Found");

        Physics.IgnoreCollision(_collider, _playerCollider, true);
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
}