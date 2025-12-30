using UnityEngine;

public class RunAction : UtilityAction
{
    public RunAction(AIContext context,ContextData contextData)
    {
        fsm = new RunFSM(context);
        considerations.Add(new ValueAboveConsideration(contextData));
    }
}
