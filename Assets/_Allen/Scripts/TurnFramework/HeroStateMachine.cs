using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PROCESSING,
    CHOOSEACTION,
    WAITING,
    ACTION,
    DEAD
}

public class HeroStateMachine : MonoBehaviour
{
    [SerializeField] private Pawn hero;

    public TurnState currentState;

    private void Update()
    {
        switch (currentState)
        {
            case TurnState.PROCESSING:
                break;
            case TurnState.CHOOSEACTION:
                break;
            case TurnState.WAITING:
                break;
            case TurnState.ACTION:
                break;
            case TurnState.DEAD:
                break;
        }
    }

}
