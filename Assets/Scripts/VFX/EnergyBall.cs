using Sirenix.OdinInspector;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] private SOEnergyBallAbilityConfig _config;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _impactParticle; // Effect spawned when projectile hits a collider

    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _projectileParticle; // Effect attached to the gameobject as child

    [SerializeField, Required, AssetsOnly] [BoxGroup("Prefabs")]
    private GameObject _muzzleParticle; // Effect instantly spawned when gameobject is spawned

    [SerializeField, Range(0f, 1f)]
    // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    private float _collideOffset = 0.15f;

    private Rigidbody _rb;
    private SphereCollider _collider;
    private ParticleSystem[] _trails;

    private Vector3 _spawnPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
        _trails = GetComponentsInChildren<ParticleSystem>();

        // register spawn position
        _spawnPoint = transform.position;

        // fly
        _rb.AddForce(transform.forward * _config.FlyingForce);
    }

    void Start()
    {
        _projectileParticle = Instantiate(_projectileParticle, transform.position, transform.rotation);
        _projectileParticle.transform.parent = transform;

        _muzzleParticle = Instantiate(_muzzleParticle, transform.position, transform.rotation);
        Destroy(_muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
    }

    void FixedUpdate()
    {
        // TODO: 
        HandleCollision();
        HandleSelfDestroy();
    }

    private void HandleSelfDestroy()
    {
        // Calculate current distance from spawn point
        var distance = Vector3.Distance(transform.position, _spawnPoint);
        if (distance >= _config.Range * 0.5f)
            ExecuteDestroyProcess(Quaternion.identity);
    }

    private void HandleCollision()
    {
        if (_rb.linearVelocity.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(_rb.linearVelocity);

        var radius = _collider.radius;
        var direction = _rb.linearVelocity.normalized;
        var detectionDistance = _rb.linearVelocity.magnitude * Time.fixedDeltaTime;

        if (Physics.SphereCast(transform.position, radius, direction, out var hit,
                detectionDistance)) // Checks if collision will happen
        {
            transform.position = hit.point + (hit.normal * _collideOffset); // Move projectile to point of collision
            ExecuteDestroyProcess(Quaternion.FromToRotation(Vector3.up, hit.normal));
        }
    }

    private void ExecuteDestroyProcess(Quaternion impactRotation)
    {
        var impactP = Instantiate(_impactParticle, transform.position, impactRotation); // Spawns impact effect

        //Component at [0] is that of the parent i.e. this object (if there is any)
        for (var i = 1; i < _trails.Length; i++) // Loop to cycle through found particle systems
        {
            var trail = _trails[i];

            if (trail.gameObject.name.Contains("Trail"))
                Destroy(trail.gameObject, 2f); // Removes the trail after seconds
        }

        Destroy(_projectileParticle, 3f); // Removes particle effect after delay
        Destroy(impactP, 3.5f); // Removes impact effect after delay
        Destroy(gameObject); // Removes the projectile
    }
}