using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

/// <summary>
/// Singleton that initializes the different Game States and sets it to Explore at the default
/// </summary>

public class StateMachine : Singleton<StateMachine>
{
    public enum State
    {
        Explore,
        Sandwich,
        Dialog,
        GameOver
    };

    public State currentGameState;

    private void Awake()
    {
        // TODO: LOAD STATE

        currentGameState = State.Explore;
    }
}
