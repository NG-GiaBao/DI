using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAIController
{
    private List<UtilityAction> actions;

    public UtilityAIController(List<UtilityAction> actions)
    {
        this.actions = actions;
    }

    public UtilityAction Choose(AIContext ctx)
    {
        UtilityAction best = null;
        float bestScore = 0f;
        foreach(var action in actions)
        {
            float score = action.CalculateScore(ctx);
            if (score > bestScore)
            {
                bestScore = score;  
                best = action;
            } 
        }
        return best;
            
    }
}
