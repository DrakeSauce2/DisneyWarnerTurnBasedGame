using System;
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
    [SerializeField] private TurnState turnState;

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

    private IEnumerator CurrentTurn()
    {       
        switch (turnState)
        {
            case TurnState.PLAYER:
                NewToggleButtons(playerPawnList[Index]);
                //CheckSwitchTurnState();
                ResetIndex();
                break; 
            case TurnState.OPPONENT:
                NewToggleButtons(opponentPawnList[Index]);
                //CheckSwitchTurnState();
                ResetIndex();
                break;
            case TurnState.ATTACKSEQUENCE:
                Debug.Log($"Starting Action Sequence!");

                ToggleOffAllButtons();
                ActionManager.Instance.InitiateActionSequence();
                CameraManager.Instance.SetCameraPosition(null);

                turnState = TurnState.PLAYER;
                ResetIndex();
                break;
            default:

                break;
        }

        yield return new WaitForSeconds(1f);

    }

    private void CheckSwitchTurnState()
    {
        List<Pawn> pawns = new List<Pawn>();
        if (turnState == TurnState.PLAYER)
            pawns = playerPawnList;
        else if (turnState == TurnState.OPPONENT)
            pawns = opponentPawnList;

        if (ActionManager.Instance.GetActionsCountInActionList() == (playerPawnList.Count + opponentPawnList.Count))
        {
            turnState = TurnState.ATTACKSEQUENCE;
            ResetIndex();
        }

        if (Index == pawns.Count)
        {
            if(turnState == TurnState.PLAYER) turnState = TurnState.OPPONENT;
            else if(turnState == TurnState.OPPONENT) turnState = TurnState.PLAYER;
            ResetIndex();
        }

        StartCoroutine(CurrentTurn());
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

    private void NewToggleButtons(Pawn pawn)
    {
        for (int i = 0; i < playerPawnList.Count; i++)
        {
            if(playerPawnList.Contains(pawn))
                playerPawnList[i].ToggleActionButtons(true);
            else
                playerPawnList[i].ToggleActionButtons(false);
        }

        for (int i = 0; i < opponentPawnList.Count; i++)
        {
            if (opponentPawnList.Contains(pawn))
                opponentPawnList[i].ToggleActionButtons(true);
            else
                opponentPawnList[i].ToggleActionButtons(false);
        }
    }

    private void ToggleOffAllButtons()
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

        StartCoroutine(CurrentTurn());
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
        CheckSwitchTurnState();
    }

    private void ResetIndex() => Index = 0;

    #endregion

}
