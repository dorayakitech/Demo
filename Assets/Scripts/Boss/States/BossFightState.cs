public class BossFightState : BossBaseState
{
    public BossFightState(BossStateMachine context) : base(context)
    {
    }

    public override void OnEnterState()
    {
        context.BossAI.ExternalBehavior = context.BossFightCfg.BossStage1AI;
        context.BossAI.RestartWhenComplete = true;
        context.BossAI.EnableBehavior();
    }

    public override void OnExitState()
    {
        context.BossAI.DisableBehavior();
        context.BossAI.ExternalBehavior = null;
    }
}