using System;
using DG.Tweening;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform A; // 物体 A，作为圆心
    public float radius = 1f; // 圆周半径
    public float duration = 2f; // 完成一圈的时间

    // private Vector3[] relativePath; // 相对路径点数组
    // private Tween circularTween; // 圆周运动的补间动画


    // void Start()
    // {
    //     // 计算相对路径点
    //     int segments = 36; // 将圆周分成的段数
    //     relativePath = new Vector3[segments];
    //     for (int i = 0; i < segments; i++)
    //     {
    //         float angle = Mathf.Deg2Rad * (i * 360f / segments);
    //         relativePath[i] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
    //     }
    //
    //     // 将物体 B 的位置设置为物体 A 的位置
    //     transform.position = A.position + radius * Vector3.forward;
    //
    //     // 创建并启动相对路径动画
    //     circularTween = transform.DOPath(relativePath, duration, PathType.CatmullRom)
    //         .SetEase(Ease.Linear)
    //         .SetLoops(-1, LoopType.Restart);
    // }
    //
    // void Update()
    // {
    //     // 每帧更新物体 B 的位置，使其跟随物体 A
    //     transform.position = A.position;
    // }
    //
    // void OnDestroy()
    // {
    //     // 销毁时停止动画
    //     if (circularTween != null && circularTween.IsActive())
    //     {
    //         circularTween.Kill();
    //     }
    // }

    private Vector3 EndPoint => -(A.forward + radius * Vector3.forward);
    private Tweener _tween;

    private void Start()
    {
        Debug.Log(EndPoint);
        _tween = transform.DOMove(EndPoint, duration);
    }

    // private void Update()
    // {
    //     _tween.ChangeEndValue(EndPoint);
    // }
}