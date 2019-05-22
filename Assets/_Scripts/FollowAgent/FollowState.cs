using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : IState
{
    public void OnEnterState(FollowAgent agent)
    {

    }

    public void OnExitState(FollowAgent agent)
    {

    }

    public void Update(FollowAgent agent)
    {
        bool shouldFollowTarget = agent.ReachedTargetFollowingThreshold();
        if (shouldFollowTarget)
        {
            agent.GetNavMeshAgent().destination = agent.GetFollowTarget().transform.position;
        }
        else
        {
            SwitchToPatrolState(agent);
        }
    }

    private void SwitchToPatrolState(FollowAgent agent)
    {
        agent.SetState(new PatrolState());
    }
}
