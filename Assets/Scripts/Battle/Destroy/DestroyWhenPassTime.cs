using UnityEngine;

[AddComponentMenu("Destroy/DestroyWhenPassTime")]
public class DestroyWhenPassTime : DestroyBaseComponent
{
    public float TimeToDestroy = 3f;
    private float _timeCount = 0f;
    
    private void Update()
    {
        _timeCount += Time.deltaTime;
        if (_timeCount >= TimeToDestroy)
        {
            DestroyHandle();
        }
    }
    
}