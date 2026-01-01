using UnityEngine;

public abstract class BaseState
{
    public abstract NameAction? Update();
    public abstract void Enter();
    public abstract void Exit() ;

  
}
