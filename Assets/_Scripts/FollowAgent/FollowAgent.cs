using UnityEngine;
using UnityEngine.AI;

public class FollowAgent : MonoBehaviour
{
    [SerializeField] private Vector3[] patrolCheckpoints;
    [SerializeField] private Transform followTarget = null;
    public float FollowTargetThresholdDistance = 10f;
    private NavMeshAgent agent = null;
    private IState currentState;
    public int LastPatrolCheckpointIndex { get; set; }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LastPatrolCheckpointIndex = -1;
        currentState = new PatrolState();
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public Vector3[] GetPatrolCheckpoints()
    {
        return patrolCheckpoints;
    }

    public void SetState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = newState;
        currentState.OnEnterState(this);
    }

    public Transform GetFollowTarget()
    {
        return followTarget;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return agent;
    }

    public bool ReachedTargetFollowingThreshold()
    {
        Vector3 currentPosition = transform.position;
        Vector3 followTargetPosition = followTarget.transform.position;
        return Vector3.Distance(currentPosition, followTargetPosition) < FollowTargetThresholdDistance;
    }
}