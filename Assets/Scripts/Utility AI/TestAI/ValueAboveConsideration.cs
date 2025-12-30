using UnityEngine;

public class ValueAboveConsideration : IConsideration
{
    private ContextData contextData;

    public ValueAboveConsideration(ContextData context)
    {
      this.contextData = context;
    }
    public float Evaluate(AIContext context)
    {
        return Mathf.Clamp01(context.GetKey(contextData));
    }
}
