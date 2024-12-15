using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStageControl : MonoBehaviour
{
    [LabelText("角色被击中"), SerializeField, AssetsOnly, Required]
    public SOEvent OnPlayerDead;
    private int _currentBattleStage = 1;
    [LabelText("Boss阶段切换事件"), SerializeField, AssetsOnly, Required]
    private GeneralIntEvent BattleStageChangeEvent;
    [LabelText("对应的环境")]
    public List<GameObject> Environments;
    
    
    private void OnEnable()
    {
        BattleStageChangeEvent.Subscribe(OnBattleStageChange);
        OnPlayerDead.Subscribe(OnPlayerDeadHandle);
    }
    
    
    [Button("测试玩家死亡")]
    private void OnPlayerDeadHandle()
    {
        Debug.Log("玩家死亡");
#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Battle");
#else
        SceneManager.LoadScene("Battle");
#endif
    }

    private void Awake()
    {
        RefreshShowByStage();
    }

    private void OnBattleStageChange(int obj)
    {
        if(_currentBattleStage == obj) return;
        _currentBattleStage = obj;
        RefreshShowByStage();
    }
    
    private void RefreshShowByStage()
    {
        Debug.Log($"当前战斗阶段：{_currentBattleStage}");
        
        //切换环境
        if (Environments != null)
        {
            for (int i = 0; i < Environments.Count; i++)
            {
                Environments[i].SetActive(_currentBattleStage == i + 1);
            }
        }
    }
    
    private void OnDisable()
    {
        BattleStageChangeEvent.Unsubscribe(OnBattleStageChange);
        OnPlayerDead.Unsubscribe(OnPlayerDeadHandle);
    }
}