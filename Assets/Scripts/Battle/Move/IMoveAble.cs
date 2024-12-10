using UnityEngine;

public interface IMoveAble
{
    void MoveToTarget(Transform target);
    void MoveToPosition(Vector3 position);
}