using UnityEngine;

public class IdleAction : BaseState
{
    public IdleAction( ) 
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle enter");
    }

    public override void Exit()
    {
        
    }

    public override NameAction? Update()
    {
       return null;
    }
}
