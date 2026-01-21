using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FsmContext
{
    private Dictionary<string, IFsmIdentity> identityDict = new();
   
    public FsmContext() { }
    
    public void RegisterIdentity(IFsmIdentity identity)
    {
        string name = identity.GetType().Name;
        if (!identityDict.ContainsKey(name))
        {
            identityDict.Add(name, identity);
        }
        else
        {
            Debug.Log("Identity already registered: ".ToColor(this, Color.brown) + name);
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
            Debug.Log("Identity not found: ".ToColor(this, Color.red) + name);
            return default;
        }
    }
}
