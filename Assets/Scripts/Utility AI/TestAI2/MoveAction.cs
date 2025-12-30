using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class MoveAction : BaseState<NPCBehavior>
{
    private NavMeshAgent agent;
    private Transform chairPos;
    private Animator animator;
    public MoveAction(NPCBehavior owner, StateMachine<NPCBehavior> fsm) : base(owner, fsm)
    {
    }

    public override void Enter()
    {
        animator.SetBool("Run", true);
        MoveTo(chairPos.position);
    }

    public override void Exit()
    {
        animator.SetBool("Sit", true);
        agent.isStopped = false;
    }

    public override void Update()
    {
        if (ReachedDestination())
        {
            Debug.Log($"Arrive {ReachedDestination()}");
            fsm.RequestAction(NameAction.Idle);
        }
    }
    public void Init(NavMeshAgent agent, Transform chairPos , Animator animator)
    {
        this.agent = agent;
        this.chairPos = chairPos;
        this.animator= animator;
    }
    public bool ReachedDestination()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }
    public void MoveTo(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }

}
