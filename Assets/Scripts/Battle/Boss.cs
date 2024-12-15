using System.Collections.Generic;
using Animancer;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class Boss : MonoBehaviour, IAnimancerOwner
{
    enum BossStage
    {
        Stage1 = 1,
        Stage2 = 2,
        Dead = 3,
    }
    [ReadOnly]
    public int Hp = 2;
    [LabelText("Boss被伤害事件"), SerializeField, AssetsOnly, Required]
    private SOEvent BossBeHurt;
    [LabelText("Boss死亡事件"), SerializeField, AssetsOnly, Required]
    private SOEvent BossBeDead;
    [LabelText("Boss阶段切换事件"), SerializeField, AssetsOnly, Required]
    private GeneralIntEvent BattleStageChangeEvent;
    [LabelText("Boss阶段1行为树")]
    public BehaviorTree BehaviorTreeInStage1;
    [LabelText("Boss阶段2行为树")]
    public BehaviorTree BehaviorTreeInStage2;
    private BossStage _currentBattleStage = BossStage.Stage1;

    private void Awake()
    {
        _animancer = GetComponent<AnimancerComponent>();
    }

    private AnimancerComponent _animancer;
    public AnimancerComponent Animancer => _animancer;

    private void OnEnable()
    {
        BossBeHurt.Subscribe(OnBossBeHurt);
    }
    
    [Button("测试进入阶段2")]
    private void TestEnterStage2()
    {
        Hp = 1;
        OnHpChange();
    }

    [Button("测试Boss被伤害")]
    private void OnBossBeHurt()
    {
        Hp--;
        OnHpChange();
    }

    private void OnHpChange()
    {
        if (Hp == 1)
        {
            _currentBattleStage = BossStage.Stage2;
            BattleStageChangeEvent.Notify(2);
        }
        else if (Hp <= 0)
        {
            _currentBattleStage = BossStage.Dead;
            Debug.Log("Boss死亡，其他地方应该监听到这个事件并处理！！！！");
            BossBeDead.Notify();
        }

        RefreshBossShow();
    }
    private void RefreshBossShow()
    {
        //切换Boss行为树
        switch (_currentBattleStage)
        {
            case BossStage.Stage1:
                SetBTActiveState(BehaviorTreeInStage1, true);
                SetBTActiveState(BehaviorTreeInStage2, false);
                break;
            case BossStage.Stage2:
                SetBTActiveState(BehaviorTreeInStage1, false);
                SetBTActiveState(BehaviorTreeInStage2, true);
                break;
            case BossStage.Dead:
                SetBTActiveState(BehaviorTreeInStage1, false);
                SetBTActiveState(BehaviorTreeInStage2, false);
                break;
        }
    }
    
    void SetBTActiveState(BehaviorTree bt, bool active)
    {
        bt.enabled = active;
        if(active)
            bt.EnableBehavior();
        else
            bt.DisableBehavior();
    }
    
    private void OnDisable()
    {
        BossBeHurt.Unsubscribe(OnBossBeHurt);
    }
}