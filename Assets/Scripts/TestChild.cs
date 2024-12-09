using System;
using UnityEngine;

public class TestChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Child OnTriggerEnter");
    }
}