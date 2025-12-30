using UnityEngine;

public class RunFSM : BaseFSM
{
    private AIContext context;
    public RunFSM(AIContext context)
    {
        this.context = context;
        
    }
    public override void Enter()
    {
        context.SetCondition(ConditionData.IsRunning, 1f);
        Debug.Log("NPC bỏ chạy vì sợ");
    }

    public override void Exit()
    {
        context.SetCondition(ConditionData.IsRunning, 0f);
        Debug.Log("NPC đã an toàn");
    }

    public override void Tick()
    {
       
    }
}
