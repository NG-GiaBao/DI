using System;
using UnityEngine;

[Serializable]
public class MoveAction : BaseState
{
    private readonly IMover agent;
    private readonly Transform chairPos;
    private readonly Transform current;
    private readonly IAnimationController animator;
    private readonly IRotate rotate;
    public MoveAction(FsmContext context, Transform target,Transform current)
    {
        agent = context.Mover;
        animator = context.Controller;
        chairPos = target;
        rotate = context.Rotate;
        this.current = current;
    }

    public override void Enter()
    {
       
        agent.SetUpdateRotation(true);
        animator.SetRun(true);
        agent.MoveTo(chairPos.position);
    }

    public override void Exit()
    {
        animator.SetSit(true);
        agent.Stop();
    }

    public override NameAction? Update()
    {
        if (agent.ReachedDestination())
        {
            agent.SetUpdateRotation(false);
            rotate.Rotate();
            if (rotate.IsRotate())
            {
                return NameAction.Idle;
            }

        }
        return null;
    }
}
