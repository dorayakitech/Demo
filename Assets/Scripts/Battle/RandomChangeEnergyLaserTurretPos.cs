using System;
using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomChangeEnergyLaserTurretPos : MonoBehaviour
{
    [LabelText("改变位置事件"), Required]
    public SOEvent ChangePosEvent;
    [LabelText("要控制的能量转发器"), Required, ItemNotNull, SceneObjectsOnly]
    public Transform[] targets;
    public float LowY = 0f;
    public float HighY = 15f;
    [FormerlySerializedAs("Duration")] [Min(0.1f), LabelText("移动时间")]
    public float MoveTime = 1f;
    private int _lowIndex = -1;

    private void Awake()
    {
        RandomSetPos();
    }

    private void OnEnable()
    {
        ChangePosEvent.Subscribe(RandomSetPos);
    }

    private void RandomSetPos()
    {
        //一个挪到低位，剩余的挪到高位
        var newLowIndex = UnityEngine.Random.Range(0, targets.Length);
        while(targets.Length > 1 && newLowIndex == _lowIndex)
            newLowIndex = UnityEngine.Random.Range(0, targets.Length);
        _lowIndex = newLowIndex;
        var low = UnityEngine.Random.Range(0, targets.Length);
        for (int i = 0; i < targets.Length; i++)
        {
            var curPos = targets[i].position;
            curPos.y = i == low ? LowY : HighY;
            targets[i].DOMove(curPos, MoveTime);
        }
    }

    private void OnDisable()
    {
        ChangePosEvent.Unsubscribe(RandomSetPos);
    }
}