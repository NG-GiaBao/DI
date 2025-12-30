using UnityEngine;

public abstract class BaseState<T> where T : class
{
    protected T owner;
    protected StateMachine<T> fsm;

    protected BaseState(T owner, StateMachine<T> fsm)
    {
        this.owner = owner;
        this.fsm = fsm;
    }
    public abstract void Update();
    public abstract void Enter();
    public abstract void Exit() ;

    protected void Request(NameAction name)
    {
        
    }
}
