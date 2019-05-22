using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private int currentPatrolCheckpointIndex;

    public void OnEnterState(FollowAgent agent)
    {
        if(agent.LastPatrolCheckpointIndex > 0)
        {
            currentPatrolCheckpointIndex = agent.LastPatrolCheckpointIndex;
        }
        else
        {
            currentPatrolCheckpointIndex = 0;
        }
    }

    public void OnExitState(FollowAgent agent)
    {
        agent.LastPatrolCheckpointIndex = this.currentPatrolCheckpointIndex;
    }

    public void Update(FollowAgent agent)
    {
        bool shouldFollowTarget = agent.ReachedTargetFollowingThreshold();
        if (shouldFollowTarget)
        {
            SwitchToFollowTargetState(agent);
        }
        else
        {
            if (agent.GetPatrolCheckpoints().Length != 0)
            {
                PerformPatrol(agent);
            }
        }
    }

    private void SwitchToFollowTargetState(FollowAgent agent)
    {
        agent.SetState(new FollowState());
    }

    private void PerformPatrol(FollowAgent agent)
    {
        NavMeshAgent navMeshAgent = agent.GetNavMeshAgent();
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetNextCheckpoint(agent);
        }
    }

    private void SetNextCheckpoint(FollowAgent agent)
    {
        int patrolCheckpointsLength = agent.GetPatrolCheckpoints().Length;
        currentPatrolCheckpointIndex = (currentPatrolCheckpointIndex + 1) % patrolCheckpointsLength;
        agent.GetNavMeshAgent().destination = agent.GetPatrolCheckpoints()[currentPatrolCheckpointIndex];
    }
}
