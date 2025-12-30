using System;
using UnityEngine;

[Serializable]
public abstract class BaseFSM
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
}
