using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    public BaseState<T> CurrentState { get; private set; }
    [SerializeField] private Dictionary<NameAction, BaseState<T>> fsmDictionary = new();
    public Action<string> OnChangeState;

    public void Register(NameAction name,BaseState<T> state)
    {
        if(!fsmDictionary.ContainsKey(name))
        {
            fsmDictionary[name] = state;
        }    
    }
    public void RequestAction(NameAction name)
    {
        if (!fsmDictionary.TryGetValue(name, out BaseState<T> state))
        {
            Debug.LogWarning($"Không có state {name} trong dict");
            return;
        }
        if (CurrentState == state) return;

        ChangeState(state);
        OnChangeState?.Invoke(CurrentState.ToString());

    }
    public void ChangeState(BaseState<T> state)
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState?.Enter();
    }
    public void Tick()
    {
        CurrentState?.Update();
    }
   public BaseState<T> GetState(NameAction name)
    {
        if(fsmDictionary.ContainsKey(name)) return fsmDictionary[name];
        return null;
    }
    
}
