using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    [SerializeField] private Transform goal = null;
    private NavMeshAgent agent = null;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    private void Update()
    {
        agent.destination = goal.position;
    }
}