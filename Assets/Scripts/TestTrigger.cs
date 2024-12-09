using DG.Tweening;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // public bool AllowMove;
    //
    // private void Update()
    // {
    //     if (AllowMove && Input.GetKeyDown(KeyCode.Space))
    //     {
    //         transform.Translate(Vector3.forward * 0.1f);
    //     }
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Trigger Publish: " + other.gameObject.name);
    // }

    public Transform EndPoint;
    public Transform StartPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start Move");
            StartPoint.DOMove(EndPoint.position, 1.0f);
        }
    }
}