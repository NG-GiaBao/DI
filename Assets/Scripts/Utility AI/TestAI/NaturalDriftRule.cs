using UnityEngine;

public class NaturalDriftRule : IContextRule
{
    private ContextData contextData;

    public NaturalDriftRule(ContextData contextData)
    {
        this.contextData = contextData;
    }
    public void Apply(AIContext ctx, float dt)
    {
        float current = ctx.GetKey(contextData); //lấy giá trị hiện tại 
        float change = contextData.KeyValue * dt; // tính lượng thay đổi 
        ContextData newData = contextData;
        newData.KeyValue = current + change;
        ctx.SetKey(newData);
    }
}
