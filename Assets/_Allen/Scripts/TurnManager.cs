using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PLAYER,
    OPPONENT,
    ATTACKSEQUENCE
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
        
        Index = 0;
    }

    private void Start()
    {
        BattleStart();
    }

    #region Turn Sequence

    private void CurrentTurn()
    {
        switch (turnState)
        {
            case TurnState.PLAYER:
                ToggleButtons(playerPawnList);
                CheckSwitchTurnState(playerPawnList);
                break; 
            case TurnState.OPPONENT:
                ToggleButtons(opponentPawnList);
                CheckSwitchTurnState(opponentPawnList);
                break;
            case TurnState.ATTACKSEQUENCE:
                Debug.Log($"Starting Action Sequence!");

                ToggleButtons(null);
                ActionManager.Instance.InitiateActionSequence();
                CameraManager.Instance.SetCameraPosition(null);

                turnState = TurnState.PLAYER;
                ResetIndex();
                break;
            default:

                break;
        }
    }

    private void CheckSwitchTurnState(List<Pawn> pawns)
    {
        if (ActionManager.Instance.GetActionsCountInActionList() == (playerPawnList.Count + opponentPawnList.Count) - 1)
        {
            turnState = TurnState.ATTACKSEQUENCE;
        }

        if (Index == pawns.Count - 1)
        {
            if(turnState == TurnState.PLAYER) turnState = TurnState.OPPONENT;
            else if(turnState == TurnState.OPPONENT) turnState = TurnState.PLAYER;
        }
    }

    private void ToggleButtons(List<Pawn> charActionButtons)
    {

        if (charActionButtons != null)
        {
            for (int i = 0; i < charActionButtons.Count; i++)
            {
                if (i == Index)
                {
                    Debug.Log($"{charActionButtons[i]}'s Turn!");

                    charActionButtons[i].ToggleActionButtons(true);
                    CameraManager.Instance.SetCameraPosition(charActionButtons[i].transform);
                }
                else
                {
                    charActionButtons[i].ToggleActionButtons(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < playerPawnList.Count; i++)
            {
                playerPawnList[i].ToggleActionButtons(false);
            }

            for (int i = 0; i < opponentPawnList.Count; i++)
            {
                opponentPawnList[i].ToggleActionButtons(false);
            }

            CameraManager.Instance.SetCameraPosition(null);
        }

    }

    #endregion

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

        CurrentTurn();
    }

    private List<GameObject> InitializePawnActionButtons(Pawn pawn)
    {
        List<GameObject> actionButtons = new List<GameObject>();

        foreach (Action action in pawn.ActionsList)
        {
            GameObject spawnedButton = UIManager.Instance.CreateButton(action.name, action.AddActionToQueue, UIManager.Instance.ActionButtonTransform);
            spawnedButton.SetActive(false);
            
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

    #region Index

    public void Next() 
    {
        Index++; 
        CurrentTurn();
    }

    private void ResetIndex() => Index = 0;

    #endregion

}
