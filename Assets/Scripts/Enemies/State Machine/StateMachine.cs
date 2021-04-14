using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState { get; private set; }
    protected Dictionary<ZombieStateType, State> States;
    private bool Running;

    private void Awake()
    {
        States = new Dictionary<ZombieStateType, State>();
    }

    public void Initialize(ZombieStateType startingState)
    {
        if (States.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
        else if (States.ContainsKey(ZombieStateType.Idle))
        {
            ChangeState(ZombieStateType.Idle);
        }
    }

    public void AddState(ZombieStateType stateName, State state)
    {
        if (States.ContainsKey(stateName))
            return;

        States.Add(stateName, state);
    }

    public void RemoveState(ZombieStateType stateName)
    {
        if (!States.ContainsKey(stateName))
            return;

        States.Remove(stateName);
    }

    public void ChangeState(ZombieStateType nextState)
    {
        if(Running)
        {
            StopRunningState();
        }

        if (!States.ContainsKey(nextState))
            return;

        currentState = States[nextState];
        currentState.Start();

        if(currentState.updateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0.0f, currentState.updateInterval);
        }

        Running = true;
    }

    private void StopRunningState()
    {
        Running = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if(Running)
        {
            currentState.IntervalUpdate();
        }
    }

    private void Update()
    {
        if (Running)
        {
            currentState.Update();
        }
    }

    private void FixedUpdate()
    {
        if (Running)
        {
            currentState.FixedUpdate();
        }
    }
}