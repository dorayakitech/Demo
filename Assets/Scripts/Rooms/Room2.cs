using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room2 : MonoBehaviour
{
    [SerializeField, Required, SceneObjectsOnly]
    [BoxGroup("Invisible Walls"), LabelText("Desync"), InfoBox("States are opposite to hidden bridge state")]
    private List<Transform> _deSyncInvisibleWalls;

    [SerializeField, Required, SceneObjectsOnly]
    [BoxGroup("Invisible Walls"), LabelText("sync"), InfoBox("States are same to hidden bridge state")]
    private List<Transform> _syncInvisibleWalls;

    [SerializeField, Required] private int _bridgeNum;

    [SerializeField, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Bridge State Change")]
    private SOLockStateChangeEvent _bridgeStateChangeEvent;

    private void OnEnable()
    {
        _bridgeStateChangeEvent.Subscribe(OnHiddenBridgeStateChanged);
    }

    private void OnDisable()
    {
        _bridgeStateChangeEvent.Unsubscribe(OnHiddenBridgeStateChanged);
    }

    private void OnHiddenBridgeStateChanged(int bridgeNum, bool show)
    {
        if (bridgeNum != _bridgeNum) return;
        HandleInvisibleWalls(show);
    }

    private void HandleInvisibleWalls(bool hiddenBridgeShow)
    {
        foreach (var invisibleWall in _deSyncInvisibleWalls)
        {
            invisibleWall.gameObject.SetActive(!hiddenBridgeShow);
        }

        foreach (var invisibleWall in _syncInvisibleWalls)
        {
            invisibleWall.gameObject.SetActive(hiddenBridgeShow);
        }
    }
}