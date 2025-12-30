using UnityEngine;

public class EatFSM : BaseFSM
{
    private AIContext context;
    public EatFSM (AIContext context)
    {
        this.context = context;
    }
    public override void Enter()
    {
        context.SetCondition(ConditionData.IsEating,1f);
        Debug.Log("NPC bắt đầu ăn");
    }

    public override void Exit()
    {
        context.SetCondition(ConditionData.IsEating, 0f);
        Debug.Log("NPC dừng ăn");
    }

    public override void Tick()
    {
        
    }
}
