using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PLAYER,
    OPPONENT,
    WAIT
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    private TurnState turnState;

    // Active Pawns Being Played
    private List<Pawn> playerPawnList = new List<Pawn>();
    private List<Pawn> opponentPawnList = new List<Pawn>();

    // 
    private List<GameObject> actionButtonsList = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Update()
    {
        switch (turnState)
        {
            case TurnState.PLAYER:

                break; 
            case TurnState.OPPONENT:
                
                break;
            case TurnState.WAIT:

                break;
            default:

                break;
        }
    }
}
