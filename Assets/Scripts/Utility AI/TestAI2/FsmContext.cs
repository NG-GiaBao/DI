using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FsmContext
{
    private Dictionary<string, IFsmIdentity> identityDict = new();
    public FsmContext(IMover mover, IAnimationController controller, IRotate rotate)
    {
        Mover = mover;
        Controller = controller;
        Rotate = rotate;
    }
    public FsmContext()
    {
    }

    public void RegisterIdentity(IFsmIdentity identity)
    {
        string name = identity.GetType().Name;
        if (!identityDict.ContainsKey(name))
        {
            identityDict.Add(name, identity);
        }
        else
        {
            Debug.Log("Identity already registered: ".ToColor(Color.brown) + name);
        }

    }
    public T GetIdentity<T>() where T : IFsmIdentity
    {
        string name = typeof(T).Name;
        if (identityDict.TryGetValue(name, out IFsmIdentity identity))
        {
            return (T)identity;
        }
        else
        {
            Debug.Log("Identity not found: ".ToColor(Color.red) + name);
            return default;
        }
    }

    public IMover Mover { get; private set; }
    public IAnimationController Controller { get; private set; }
    public IRotate Rotate { get; private set; }
}
