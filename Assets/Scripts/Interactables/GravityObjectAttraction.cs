using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GravityObjectAttraction : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Set Target")]
    private SOGameObjectNotifiedEvent _setTargetEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Unset Target")]
    private SOGameObjectNotifiedEvent _unsetTargetEvent;

    [SerializeField, Required] [InfoBox("Distance between this object and player left hand in Y Axis")]
    private Vector3 _playerLeftHandOffset;

    [SerializeField, Required] private float _moveDuration = 1.0f;

    private Collider _objCollider;
    private CapsuleCollider _playerCollider;
    private GameObject _playerLeftHand;
    private bool _isAttractionTarget;
    private Tweener _moveTweener;

    private void Awake()
    {
        _objCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerLeftHand,
                out _playerLeftHand))
            Debug.LogError("Gravity Obj Moving Follow Point Not Found");

        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerCollider, out _playerCollider))
            Debug.LogError("Player Collider Not Found");
    }

    private void OnEnable()
    {
        _setTargetEvent.Subscribe(OnSetTarget);
        _unsetTargetEvent.Subscribe(OnUnsetTarget);
    }

    private void OnDisable()
    {
        _setTargetEvent.Unsubscribe(OnSetTarget);
        _unsetTargetEvent.Unsubscribe(OnUnsetTarget);
    }

    private void Update()
    {
        HandleAttraction(CalculateMoveEndPoint());
    }

    private void OnSetTarget(GameObject target)
    {
        if (target != gameObject) return;

        _isAttractionTarget = true;
        ActivateCollisionBetweenPlayer(false);
    }

    private void OnUnsetTarget(GameObject target)
    {
        if (target != gameObject) return;

        _isAttractionTarget = false;
        ActivateCollisionBetweenPlayer(true);

        _moveTweener?.Kill();
        _moveTweener = null;
    }

    private void HandleAttraction(Vector3 moveEndPoint)
    {
        if (!_isAttractionTarget) return;
        if (_moveTweener == null)
        {
            _moveTweener = transform.DOMove(moveEndPoint, _moveDuration);
        }
        else
        {
            _moveTweener.ChangeEndValue(moveEndPoint, true);
        }
    }

    private Vector3 CalculateMoveEndPoint()
    {
        var handForward = -_playerLeftHand.transform.right;
        handForward.y = 0.0f;
        handForward.Normalize();
        return _playerLeftHand.transform.position + handForward * _playerLeftHandOffset.z +
               Vector3.up * _playerLeftHandOffset.y;
    }

    private void ActivateCollisionBetweenPlayer(bool active)
    {
        Physics.IgnoreCollision(_objCollider, _playerCollider, !active);
    }
}