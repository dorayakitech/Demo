using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Prefab;
    public float Force;

    // private Rigidbody rb;

    private void Start()
    {
        // rb = Prefab.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Nova");
            // rb.linearVelocity = new Vector3(0, 0, 0.2f);
            var nova = Instantiate(Prefab, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
            nova.GetComponent<Rigidbody>().AddForce(Vector3.forward * Force);
            // rb.AddForce(transform.forward * Force);
        }
    }
}