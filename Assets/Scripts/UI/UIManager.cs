using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, Required, SceneObjectsOnly]
    private PlayerDeathPanel _playerDeathPanel;

    private void Awake()
    {
        _playerDeathPanel.SetEnable(false);
        _playerDeathPanel.Callback = () => { _playerDeathPanel.SetEnable(false); };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _playerDeathPanel.SetEnable(true);
        }
    }
}