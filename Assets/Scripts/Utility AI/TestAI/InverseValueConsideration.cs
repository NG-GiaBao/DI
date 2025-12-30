using UnityEngine;

public class InverseValueConsideration : IConsideration
{
    private ContextData contextData;

    public InverseValueConsideration(ContextData contextData)
    {
       this.contextData = contextData;
    }
    public float Evaluate(AIContext context)
    {
        return 1f - Mathf.Clamp01(context.GetKey(contextData));
    }
}
