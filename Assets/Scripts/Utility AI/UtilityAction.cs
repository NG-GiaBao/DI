using System.Collections.Generic;
using UnityEngine;

public abstract class UtilityAction
{
    protected BaseFSM fsm;
    protected List<IConsideration> considerations = new();
    public float CalculateScore(AIContext ctx)
    {
        float score = 1f;
        foreach(var c in considerations)
        {
            score *= c.Evaluate(ctx);
        }    
        return score;
    }
    
    public void Start()
    {
        fsm.Enter();
    }
    public void Tick()
    {
        fsm.Tick();
    }
    public void Stop()
    {
        fsm.Exit();
    }
}
