using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : HealthComponent
{
    private StateMachine zombieStateMachine;

    private void Awake()
    {
        zombieStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        //base.Destroy();

        zombieStateMachine.ChangeState(ZombieStateType.Dead);
    }
}
