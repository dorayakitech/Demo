using Animancer.FSM;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(GravityObjectStaticState))]
[RequireComponent(typeof(GravityObjectElevateState))]
[RequireComponent(typeof(GravityObjectFallState))]
public class GravityObjectStateMachine : MonoBehaviour
{
    [SerializeField, Required, SceneObjectsOnly]
    private GameObject _mainObj;

    [SerializeField, Required] [BoxGroup("States")]
    private GravityObjectBaseState _static;

    [SerializeField, Required] [BoxGroup("States")]
    private GravityObjectBaseState _elevate;

    [SerializeField, Required] [BoxGroup("States")]
    private GravityObjectBaseState _fall;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Set Target")]
    private SOGameObjectNotifiedEvent _setTargetEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Unset Target")]
    private SOGameObjectNotifiedEvent _unsetTargetEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Touch Ground")]
    private SOGameObjectNotifiedEvent _touchGroundEvent;

    private readonly StateMachine<GravityObjectBaseState>.WithDefault _stateMachine = new();

    private Collider _mainObjCollider;
    private CapsuleCollider _playerCollider;
    private GameObject _playerLeftHand;

    public GameObject MainObj => _mainObj;
    public GameObject PlayerLeftHand => _playerLeftHand;

    private void Awake()
    {
        // This collider is on child GameObject, not parent GameObject, although the name is "main obj collider"!
        _mainObjCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _setTargetEvent.Subscribe(OnSetTarget);
        _unsetTargetEvent.Subscribe(OnUnsetTarget);
        _touchGroundEvent.Subscribe(OnTouchGround);
    }

    private void OnDisable()
    {
        _setTargetEvent.Unsubscribe(OnSetTarget);
        _unsetTargetEvent.Unsubscribe(OnUnsetTarget);
        _touchGroundEvent.Unsubscribe(OnTouchGround);
    }

    private void Start()
    {
        // get player collider
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerCollider, out _playerCollider))
            Debug.LogError("Player Collider Not Found");

        // get player left hand
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerLeftHand,
                out _playerLeftHand))
            Debug.LogError("Gravity Obj Moving Follow Point Not Found");

        _stateMachine.DefaultState = _static;
    }

    private void OnSetTarget(GameObject target)
    {
        if (target != gameObject) return;
        _stateMachine.TrySetState(_elevate);
    }

    private void OnUnsetTarget(GameObject target)
    {
        if (target != gameObject) return;
        _stateMachine.TrySetState(_fall);
    }

    private void OnTouchGround(GameObject target)
    {
        if (target != gameObject) return;
        _stateMachine.TrySetState(_static);
    }

    public void IgnoreCollisionBetweenPlayer(bool ignore)
    {
        Physics.IgnoreCollision(_mainObjCollider, _playerCollider, ignore);
    }
}