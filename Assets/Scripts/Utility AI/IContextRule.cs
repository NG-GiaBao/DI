using UnityEngine;

public interface IContextRule
{
    void Apply(AIContext ctx, float dt);
}
