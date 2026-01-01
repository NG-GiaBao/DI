using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    [field: SerializeField] public NameAction currentAction;
    public BaseState CurrentState { get; private set; }
    [SerializeField] private Dictionary<NameAction, BaseState> states = new();

    public void Register(NameAction name, BaseState state)
    {
        if (!states.ContainsKey(name))
        {
            states[name] = state;
        }
    }
   
    public void ChangeState(NameAction state)
    {
        if (!states.TryGetValue(state, out var newState)) return;
        currentAction = state;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Tick()
    {
        var next = CurrentState.Update();
        if (next.HasValue)
        {
            ChangeState(next.Value);
        }
    }
   
}
