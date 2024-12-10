using UnityEngine;

public class EnergyBallReceiver : MonoBehaviour, IInteractable
{
    public GameObject Obj => gameObject;

    public void IsDetected()
    {
        Debug.Log($"{gameObject.name} Is detected");
    }

    public void IsUndetected()
    {
        Debug.Log($"{gameObject.name} Is undetected");
    }

    public void IsSetTarget()
    {
        Debug.Log($"{gameObject.name} Is set target");
    }

    public void IsUnsetTarget()
    {
        Debug.Log($"{gameObject.name} Is unset target");
    }
}