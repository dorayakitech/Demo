using Sirenix.OdinInspector;
using UnityEngine;

public class PressedPlate : MonoBehaviour
{
    [SerializeField, Required] private Transform _checkPoint;
    [SerializeField, Required] private float _checkRange = 0.1f;

    private FlashVFX _flashVFX;
    private int _checkedLayerMasks;
    private bool _isPressed;

    private void Awake()
    {
        _flashVFX = GetComponent<FlashVFX>();
        _checkedLayerMasks = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        ChangeAppearance();
    }

    private bool IsPressed()
    {
        return Physics.CheckSphere(_checkPoint.position, _checkRange, _checkedLayerMasks);
    }

    private void ChangeAppearance()
    {
        var currentPressed = IsPressed();

        switch (currentPressed)
        {
            case true when !_isPressed:
                _flashVFX.StartFlash();
                break;
            case false when _isPressed:
                _flashVFX.StopFlash();
                break;
        }

        _isPressed = currentPressed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_checkPoint.position, _checkRange);
    }
}