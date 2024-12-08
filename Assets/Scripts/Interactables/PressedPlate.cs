using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PressedPlate : MonoBehaviour
{
    [SerializeField, Required] private Transform _checkPoint;
    [SerializeField, Required] private float _checkRange = 0.1f;

    private int _checkedLayerMasks;

    private void Awake()
    {
        _checkedLayerMasks = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        if (IsPressed())
        {
            Debug.Log("Pressed");
        }
    }

    private bool IsPressed()
    {
        return Physics.OverlapSphere(_checkPoint.position, _checkRange, _checkedLayerMasks).Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_checkPoint.position, _checkRange);
    }
}