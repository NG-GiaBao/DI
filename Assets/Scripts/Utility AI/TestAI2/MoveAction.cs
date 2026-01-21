using System;
using UnityEngine;

[Serializable]
public class MoveAction : BaseState
{
    private readonly IMover agent;
    private readonly Transform chairPos;
    private readonly IAnimationController animator;
    private readonly IRotate rotate;
    public MoveAction(FsmContext context, Transform target)
    {
        agent = context.GetIdentity<NavMeshMover>();
        if(agent ==null) Debug.Log("Agent not found".ToColor(this, Color.red));
      
        animator = context.GetIdentity<AnimatorController>();
        if (animator == null) Debug.Log("animator not found".ToColor(this, Color.red));

        rotate = context.GetIdentity<TranformRotate>();
        if (rotate == null) Debug.Log("rotate not found".ToColor(this, Color.red));

        chairPos = target;
    }

    public override void Enter()
    {
        agent.SetUpdateRotation(true);
        animator.SetBool("Run",true);
        agent.MoveTo(chairPos.position);
    }

    public override void Exit()
    {
        animator.SetBool("Sit", true);
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
