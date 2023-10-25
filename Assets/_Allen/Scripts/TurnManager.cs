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

    [Header("Action Button Prefab")]
    [SerializeField] private GameObject turnActionButtonPrefab;

    // Active Pawns Being Played
    private List<Pawn> playerPawnList = new List<Pawn>();
    private List<Pawn> opponentPawnList = new List<Pawn>();

    public int Index { get; private set; }

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

    #region Start Of Battle Functions

    private void BattleStart()
    {
        // We Iterate through both incase one team has a different amount of members
        for (int i = 0; i < playerPawnList.Count; i++)
        {
            playerPawnList[i].SetActionButtons(InitializePawnActionButtons(playerPawnList[i]));
        }

        for (int i = 0; i < opponentPawnList.Count; i++)
        {
            opponentPawnList[i].SetActionButtons(InitializePawnActionButtons(opponentPawnList[i]));
        }
    }

    private List<GameObject> InitializePawnActionButtons(Pawn pawn)
    {
        List<GameObject> actionButtons = new List<GameObject>();

        foreach (Action action in pawn.ActionsList)
        {
            GameObject spawnedButton = Instantiate(turnActionButtonPrefab);
            spawnedButton.GetComponent<TurnActionButton>().Init(action.StartAction, action.name);
            actionButtons.Add(spawnedButton);
        }

        return actionButtons;
    }

    public void SetPlayerPawns(List<Pawn> pawns)
    {
        playerPawnList = pawns;
    }

    public void SetOpponentPawns(List<Pawn> pawns)
    {
        opponentPawnList = pawns;
    }

    #endregion

    private void StartTurn(Pawn pawn)
    {

    }

    public void Next() => Index++;
    private void ResetIndex() => Index = 0;

}
