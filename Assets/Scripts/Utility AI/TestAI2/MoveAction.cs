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
        agent = context.GetIdentity<NavMeshMover>();
        if(agent !=null) Debug.Log("Agent found".ToColor(Color.yellow));
      
        animator = context.GetIdentity<AnimatorController>();
        if (animator != null) Debug.Log("animator found".ToColor(Color.yellow));
        chairPos = target;
        rotate = context.GetIdentity<TranformRotate>();
        if (rotate != null) Debug.Log("rotate found".ToColor(Color.yellow));
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
