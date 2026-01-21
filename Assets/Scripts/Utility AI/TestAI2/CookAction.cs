using UnityEngine;

public class CookAction : BaseState
{
    private readonly IAnimationController controller;
    private readonly MotherBehavior.DataAction dataAction;

    public CookAction(FsmContext context, MotherBehavior.DataAction dataAction)
    {
        this.controller = context.GetIdentity<AnimatorController>();
        this.dataAction = dataAction;
    }

    public override void Enter()
    {
        Debug.Log("Cook enter".ToColor(this, Color.yellow));
        controller.SetBool("Cook", true);
    }

    public override void Exit()
    {

    }

    public override NameAction? Update()
    {
        if (controller.IsAnimFinished("Cook"))
        {
            controller.SetBool("Cook", false);
            dataAction.itemPref.SetActive(true);
            return NameAction.Idle;
        }
        return null;
    }
}
