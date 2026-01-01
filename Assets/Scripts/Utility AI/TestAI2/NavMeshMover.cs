using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : IMover
{
    private readonly NavMeshAgent agent;

    public NavMeshMover(NavMeshAgent agent)
    {
        this.agent = agent;
    }
    public void MoveTo(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }

    public bool ReachedDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public void SetUpdateRotation(bool value)
    {
        agent.updateRotation = value;
    }

    public bool IsInFront(Transform from, Transform to)
    {
        Vector3 toPlayer = (to.position - from.position).normalized;
        float dot = Vector3.Dot(from.forward, toPlayer);
        Debug.Log($"dot {dot}");
        return dot > 0.7f;
    }
}
