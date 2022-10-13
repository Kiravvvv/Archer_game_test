using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_state : State
{

    public Test_state(Game_character_abstract character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter_state()
    {
        base.Enter_state();
        Debug.Log("Стандартное состояние запущено");
    }
}
