using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Publish")]
    private SOPlayerInputValueChangedEvent _inputValueChangedEvent;

    private PlayerInputActions _inputActions;
    private PlayerInputValue _inputValue;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        Subscribe();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        Unsubscribe();
    }

    private void OnChangeMoveVector(InputAction.CallbackContext ctx)
    {
        _inputValue.MoveVector = ctx.ReadValue<Vector2>();
        _inputValueChangedEvent.Notify(_inputValue);
    }

    private void OnChangeIsPressingUseAbility(InputAction.CallbackContext ctx)
    {
        _inputValue.IsPressingUseAbility = ctx.ReadValueAsButton();
        _inputValueChangedEvent.Notify(_inputValue);
    }

    private void Subscribe()
    {
        _inputActions.Player.Move.performed += OnChangeMoveVector;
        _inputActions.Player.Move.canceled += OnChangeMoveVector;
        _inputActions.Player.UseAbility.performed += OnChangeIsPressingUseAbility;
        _inputActions.Player.UseAbility.canceled += OnChangeIsPressingUseAbility;
    }

    private void Unsubscribe()
    {
        _inputActions.Player.Move.performed -= OnChangeMoveVector;
        _inputActions.Player.Move.canceled -= OnChangeMoveVector;
        _inputActions.Player.UseAbility.performed -= OnChangeIsPressingUseAbility;
        _inputActions.Player.UseAbility.canceled -= OnChangeIsPressingUseAbility;
    }
}

public struct PlayerInputValue
{
    public Vector2 MoveVector;
    public bool IsPressingUseAbility;
}