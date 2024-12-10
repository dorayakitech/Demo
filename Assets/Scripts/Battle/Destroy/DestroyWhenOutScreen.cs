using UnityEngine;
//飞出屏幕后销毁
[AddComponentMenu("Destroy/DestroyWhenOutScreen")]
public class DestroyWhenOutScreen : DestroyBaseComponent
{
    private void OnBecameInvisible()
    {
        DestroyHandle();
    }
}