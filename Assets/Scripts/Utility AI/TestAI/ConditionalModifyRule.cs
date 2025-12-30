using UnityEngine;

public class ConditionalModifyRule : IContextRule
{
    private ContextData contextData;

    public ConditionalModifyRule(ContextData data)
    {
        contextData = data;
    }
    public void Apply(AIContext ctx, float dt)
    {
        if (ctx.GetCondition(contextData) > 0f)
        {
            float current = ctx.GetKey(contextData);
            float change = contextData.ConditionValue * dt;
            ContextData newData = contextData;
            newData.KeyValue = current + change;
            ctx.SetKey(newData);
        }
    }
}
