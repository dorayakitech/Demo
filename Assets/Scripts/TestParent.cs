using System;
using UnityEngine;

public class TestParent : MonoBehaviour
{
    public bool AllowMove;
    
    private void Update()
    {
        if (AllowMove && Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.back * 0.1f);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Parent OnTriggerEnter");
    // }
}