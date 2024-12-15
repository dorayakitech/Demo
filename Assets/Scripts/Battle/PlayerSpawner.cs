using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Required, AssetsOnly]
    public GameObject playerPrefab;
    [Required, SceneObjectsOnly]
    public Transform spawnPoint;

    private void Awake()
    {
        var player = FindAnyObjectByType<Player>();
        //如果玩家已经存在了，直接返回
        if (player != null)
        {
            player.gameObject.SetActive(true);
            Player.Instance.Rb.position = spawnPoint.position;
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
        else
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);//实例化玩家
        
    }
}