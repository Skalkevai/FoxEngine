using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachineBrain : MonoBehaviour
{
    [SerializeField] private State defaultState;
    private State currentState;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }

    public void ChangeState(State _nextState,Dictionary<string, object> _params = null)
    {
        currentState?.OnExitState();
        DestroyImmediate(currentState, true);
        currentState = null;

        currentState = InstantiateState(_nextState);
        currentState?.SetParams(_params);
        currentState?.OnEnterState(this);
    }

    private State InstantiateState(State _state)
    {
        if (!_state)
            return null;

        State newState = ScriptableObject.Instantiate(_state);
        newState.owner = this;

        return newState;
    }

    public void ResetState()
    {
        currentState?.OnExitState();
        DestroyImmediate(currentState,true);
        currentState = null;

        currentState = InstantiateState(defaultState);
        currentState?.OnEnterState(this);
    }
}

public abstract class State : ScriptableObject
{
    [HideInInspector] public StateMachineBrain owner;

    public Vector3 Position => owner.transform.position;

    public virtual void OnEnterState(StateMachineBrain _owner)
    {
        owner = _owner;
    }

    public virtual void SetParams(Dictionary<string,object> _params)
    { 
        
    }

    public virtual void UpdateState()
    {
    }

    public virtual void FixedUpdateState()
    {
    }

    public virtual void OnExitState()
    {
        owner = null;
    }
}

public abstract class AgentState : State
{
    [HideInInspector] public new StateMachineAgentBrain owner;

    public override void OnEnterState(StateMachineBrain _owner)
    {
        base.OnEnterState(_owner);
        owner = _owner as StateMachineAgentBrain;
    }
}
