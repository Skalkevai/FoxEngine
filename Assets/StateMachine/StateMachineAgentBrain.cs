using UnityEngine;
using UnityEngine.AI;

public class StateMachineAgentBrain : StateMachineBrain
{
    [SerializeField] private NavMeshAgent agent;

    public NavMeshAgent Agent => agent;
}
