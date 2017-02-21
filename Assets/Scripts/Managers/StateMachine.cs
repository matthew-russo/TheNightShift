using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class StateMachine : Singleton<StateMachine>
{
    public enum State
    {
        Explore,
        Sandwich,
        Dialog,
    };

    public State currentGameState;

    private void Awake()
    {
        // TODO: LOAD STATE

        currentGameState = State.Dialog;
    }
}
