using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GenerateGravityObjectComponent : MonoBehaviour
{
    [Required, AssetsOnly, LabelText("生成重物事件")]
    public SOEvent GenerateEvent;
    [Required, AssetsOnly, LabelText("重物的预设")]
    public GameObject GravityObjectPrefab;

    public float SpawnHeight = 10f;
    public int GenerateCount;
    //设定一个范围，在Gizmos里绘制出来
    public Vector2 zone = new Vector2(10, 10);

    private List<GameObject> _curList = new List<GameObject>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(transform.position, new Vector3(zone.x, SpawnHeight, zone.y));
        var halfSize = new Vector3(zone.x / 2, SpawnHeight / 2, zone.y / 2);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, SpawnHeight / 2, 0), halfSize * 2);
    }

    private void OnEnable()
    {
        GenerateEvent.Subscribe(GenerateGravityObject);
    }

    [Button("测试生成重物")]
    private void GenerateGravityObject()
    {
        //在场景内生成指定数量的重物。老的重物挂上溶解组件，并延迟干掉
        if(_curList.Count > 0)
        {
            foreach (var go in _curList)
            {
                // var dissolve = go.AddComponent<DissolveComponent>();
                // dissolve.DissolveTime = 1f;
                Destroy(go, 1.1f);
            }
            _curList.Clear();
        }
        
        for (int i = 0; i < GenerateCount; i++)
        {
            var pos = transform.position + new Vector3(UnityEngine.Random.Range(-zone.x / 2, zone.x / 2), SpawnHeight, UnityEngine.Random.Range(-zone.y / 2, zone.y / 2));
            var go = Instantiate(GravityObjectPrefab, pos, Quaternion.identity);
            _curList.Add(go);
        }
    }

    private void OnDisable()
    {
        GenerateEvent.Unsubscribe(GenerateGravityObject);
    }
}