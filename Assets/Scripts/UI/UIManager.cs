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

    [SerializeField, Required, SceneObjectsOnly]
    private AbilityPanel _abilityPanel;

    [SerializeField, Required, SceneObjectsOnly]
    private PausePanel _pausePanel;

    [SerializeField, Required, SceneObjectsOnly]
    private StartPanel _startPanel;

    [SerializeField, Required, SceneObjectsOnly]
    private IntroPanel _introPanel;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribe"), LabelText("Show Popup")]
    private SOShowPopupEvent _showPopupEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Show Ability")]
    private SOShowAbilityIndicatorEvent _showAbilityEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Conversation Start")]
    private SOEvent _genericConversationStartEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Conversation End")]
    private SOEvent _genericConversationEndEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Player Press Pause")]
    private SOEvent _playerPressPauseEvent;

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
        SubscribeInputActions();

        _showPopupEvent.Subscribe(OnShowPopup);
        _showAbilityEvent.Subscribe(OnShowAbilityPanel);
        _genericConversationStartEvent.Subscribe(OnHideAbilityPanel);
        _genericConversationEndEvent.Subscribe(OnReShowAbilityPanel);
        _playerPressPauseEvent.Subscribe(OnShowPauseMenu);
    }

    private void OnDisable()
    {
        UnsubscribeInputActions();

        _showPopupEvent.Unsubscribe(OnShowPopup);
        _showAbilityEvent.Unsubscribe(OnShowAbilityPanel);
        _genericConversationStartEvent.Unsubscribe(OnHideAbilityPanel);
        _genericConversationEndEvent.Unsubscribe(OnReShowAbilityPanel);
        _playerPressPauseEvent.Unsubscribe(OnShowPauseMenu);
    }

    private void Start()
    {
        // ShowStartPanel();
        ShowIntroPanel();
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

        EnableUIInputAndDisablePlayerInput(true);
    }

    private void OnShowAbilityPanel(string abilityName)
    {
        _abilityPanel.ShowAbility(abilityName);
    }

    private void OnHideAbilityPanel()
    {
        _abilityPanel.HideAbility();
    }

    private void OnReShowAbilityPanel()
    {
        _abilityPanel.ReShow();
    }

    private void OnShowPauseMenu()
    {
        _pausePanel.Show();
        _activePanel = _pausePanel;

        Time.timeScale = 0.0f;
        EnableUIInputAndDisablePlayerInput(true);
    }

    private void OnSelectPauseButton(InputAction.CallbackContext ctx)
    {
        var inputVal = ctx.ReadValue<Vector2>();

        var idx = inputVal.y switch
        {
            > 0 => -1,
            < 0 => 1,
            _ => 0
        };
        _pausePanel.SelectButton(idx);
    }

    private void SubscribeInputActions()
    {
        _inputActions.UI.Continue.started += OnPressContinue;
        _inputActions.UI.SelectMenuButton.started += OnSelectPauseButton;
    }

    private void UnsubscribeInputActions()
    {
        _inputActions.UI.Continue.started -= OnPressContinue;
        _inputActions.UI.SelectMenuButton.started -= OnSelectPauseButton;
    }

    private void ShowStartPanel()
    {
        _startPanel.Show();
        _activePanel = _startPanel;
        EnableUIInputAndDisablePlayerInput(true);
    }

    private void ShowIntroPanel()
    {
        _introPanel.Show();
        _activePanel = _introPanel;
        EnableUIInputAndDisablePlayerInput(true);
    }

    private void SetCallbacks()
    {
        HandlePopupPanelCallback();
        HandlePausePanelCallback();
        HandleStartPanelCallback();
        HandleIntroPanelCallback();
    }

    private void HandlePopupPanelCallback()
    {
        _popupPanel.OnClose = tasks =>
        {
            EnableUIInputAndDisablePlayerInput(false);

            foreach (var task in tasks)
            {
                task.Execute(this);
            }
        };
    }

    private void HandlePausePanelCallback()
    {
        _pausePanel.OnConfirm = buttonType =>
        {
            switch (buttonType)
            {
                case PausePanel.ButtonType.Continue:
                    _pausePanel.Hide();
                    Time.timeScale = 1.0f;
                    break;

                case PausePanel.ButtonType.QuitGame:
                    Application.Quit();
                    break;
            }

            EnableUIInputAndDisablePlayerInput(false);
        };
    }

    private void HandleStartPanelCallback()
    {
        _startPanel.OnEnd = tasks =>
        {
            EnableUIInputAndDisablePlayerInput(false);

            foreach (var task in tasks)
            {
                task.Execute(this);
            }
        };
    }

    private void HandleIntroPanelCallback()
    {
        _introPanel.OnEnd = tasks =>
        {
            EnableUIInputAndDisablePlayerInput(false);

            foreach (var task in tasks)
            {
                task.Execute(this);
            }
        };
    }

    private void EnableUIInputAndDisablePlayerInput(bool yes)
    {
        if (yes)
            _inputActions.Enable();
        else
            _inputActions.Disable();

        Player.Instance.InputManager.SetEnableState(!yes);
    }
}