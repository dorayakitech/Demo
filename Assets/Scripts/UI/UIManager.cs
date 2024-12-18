using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[ShowOdinSerializedPropertiesInInspector]
public class UIManager : Singleton<UIManager>
{
    [SerializeField, Required, SceneObjectsOnly]
    private PlayerDeathPanel _playerDeathPanel;

    [SerializeField, Required, SceneObjectsOnly]
    private PopupPanel _popupPanel;

    [SerializeField, Required] private Dictionary<string, SOPopupPanelConfig> _popupPanelConfigs = new();

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribe"), LabelText("Show Popup")]
    private SOShowPopupEvent _showPopupEvent;

    private PlayerInputActions _inputActions;
    private IPanel _activePanel;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Disable();

        SetCallbacks();
    }

    private void OnEnable()
    {
        _inputActions.UI.Continue.started += OnPressContinue;

        _showPopupEvent.Subscribe(OnShowPopup);
    }

    private void OnDisable()
    {
        _inputActions.UI.Continue.started -= OnPressContinue;

        _showPopupEvent.Unsubscribe(OnShowPopup);
    }

    private void OnPressContinue(InputAction.CallbackContext ctx)
    {
        _activePanel.Continue();
    }

    private void OnShowPopup(SOPopupPanelConfig config)
    {
        _popupPanel.Config = config;
        _popupPanel.Show();
        _activePanel = _popupPanel;

        _inputActions.Enable();
        Player.Instance.InputManager.SetEnableState(false);
    }

    private void SetCallbacks()
    {
        _popupPanel.OnClose = () =>
        {
            _inputActions.Disable();
            Player.Instance.InputManager.SetEnableState(true);
        };
    }
}