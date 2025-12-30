using System.Collections.Generic;
using UnityEngine;

public class ContextUpdater 
{
    private List<IContextRule> rules = new();

    public void AddRule(IContextRule rule)
    {
        rules.Add(rule);
    }
    public void Tick(AIContext ctx,float dt)
    {
        foreach (var rule in rules)
        {
            rule.Apply(ctx,dt);
        } 
            
    }
}
