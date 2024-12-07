using Sirenix.OdinInspector;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _impactParticle;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _projectileParticle;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _muzzleParticle;

    [SerializeField, Required] private float _flyForce;
    [SerializeField, Required] private float _maxFlyDistance;

    // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    [SerializeField, Range(0f, 1f)] private float _collideOffset = 0.15f;

    private Rigidbody _rb;
    private SphereCollider _collider;
    private ParticleSystem[] _trails;

    private Vector3 _spawnPoint;
    private float _ballRadius;
    private Vector3 _ballFlyingDirection;
    private float _collisionDetectionDistance;

    private bool _hasTarget;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
        _trails = GetComponentsInChildren<ParticleSystem>();

        _ballRadius = _collider.radius;

        // register spawn position
        _spawnPoint = transform.position;
    }

    void Start()
    {
        _projectileParticle = Instantiate(_projectileParticle, transform.position, transform.rotation);
        _projectileParticle.transform.parent = transform;

        _muzzleParticle = Instantiate(_muzzleParticle, transform.position,
            transform.rotation * Quaternion.Euler(0.0f, 180.0f, 0.0f));
        _muzzleParticle.transform.parent = transform;
        Destroy(_muzzleParticle, 1.5f);

        // Handle Fly
        Fly();
    }

    void FixedUpdate()
    {
        if (_hasTarget)
            HandleCollision();
        else
            HandleSelfDestroy();
    }

    private void Fly()
    {
        _hasTarget = GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.TargetEnergyBallSwitch,
            out GameObject targetSwitch);

        var flyDir = _hasTarget ? (targetSwitch.transform.position - transform.position).normalized : transform.forward;
        _rb.AddForce(flyDir * _flyForce);
    }

    private void HandleSelfDestroy()
    {
        // Calculate current distance from spawn point
        var distance = Vector3.Distance(transform.position, _spawnPoint);
        if (distance >= _maxFlyDistance)
            ExecuteDestroyProcess(Quaternion.identity, true);
    }

    private void HandleCollision()
    {
        if (_rb.linearVelocity.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(_rb.linearVelocity);

        _ballFlyingDirection = _rb.linearVelocity.normalized;
        _collisionDetectionDistance = _rb.linearVelocity.magnitude * Time.fixedDeltaTime;

        // Interactive Sphere cannot be detected since the ball is spawned inside the Sphere. They're overlapping
        if (Physics.SphereCast(transform.position, _ballRadius, _ballFlyingDirection, out var hit,
                _collisionDetectionDistance))
        {
            // Move projectile to point of collision
            transform.position = hit.point + (hit.normal * _collideOffset);
            ExecuteDestroyProcess(Quaternion.FromToRotation(Vector3.up, hit.normal));
        }
    }

    private void ExecuteDestroyProcess(Quaternion impactRotation, bool selfDestroy = false)
    {
        //Loop index starts from 1 since component at [0] is that of the parent i.e. this object
        for (var i = 1; i < _trails.Length; i++)
        {
            var trail = _trails[i];

            if (trail.gameObject.name.Contains("Trail"))
                Destroy(trail.gameObject, 2f);
        }

        Destroy(_projectileParticle, 3f);
        Destroy(gameObject);

        if (!selfDestroy)
        {
            var impactP = Instantiate(_impactParticle, transform.position, impactRotation);
            Destroy(impactP, 3.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _ballRadius);
        Vector3 endPosition = transform.position + _ballFlyingDirection.normalized * _collisionDetectionDistance;
        Gizmos.DrawWireSphere(endPosition, _ballRadius);
        Gizmos.DrawLine(transform.position, endPosition);
    }
}