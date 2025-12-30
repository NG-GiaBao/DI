using UnityEngine;

public class IdleAction : BaseState<NPCBehavior>
{
    public IdleAction(NPCBehavior owner, StateMachine<NPCBehavior> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle enter");
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
       
    }
}
