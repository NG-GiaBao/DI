using UnityEngine;

public class EatAction : UtilityAction
{
    public EatAction(AIContext context,ContextData hunger , ContextData fear)
    {
        fsm = new EatFSM(context);
        considerations.Add(new ValueAboveConsideration(hunger));
        considerations.Add(new InverseValueConsideration(fear));
    }
}
