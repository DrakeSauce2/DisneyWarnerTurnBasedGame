using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PLAYER,
    OPPONENT,
    TARGETING,
    ATTACKSEQUENCE
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    [SerializeField] private TurnState turnState;

    [Header("Targeting")]
    [SerializeField] private Transform currentTurnTarget;
    [SerializeField] private Transform oppenentTargeter;
    [SerializeField] private int selectionIndex = 0;

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

    private void Update()
    {
        if (turnState != TurnState.TARGETING) return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncrementSelection(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncrementSelection(-1);
        }

        oppenentTargeter.position = SelectTarget(opponentPawnList);
    }

    private void IncrementSelection(int increment)
    {
        selectionIndex += increment;

        if (selectionIndex >= opponentPawnList.Count)
        {
            selectionIndex = 0;
        }

        if (selectionIndex < 0)
        {
            selectionIndex = opponentPawnList.Count - 1;
        }

    }

    #region Turn Sequence

    private IEnumerator CurrentTurn()
    {       
        switch (turnState)
        {
            case TurnState.PLAYER:
                currentTurnTarget.position = playerPawnList[Index].transform.position;
                ToggleButtons(playerPawnList[Index]);
                CameraManager.Instance.SetCameraPosition(playerPawnList[Index].transform);
                ResetIndex();
                break; 
            case TurnState.OPPONENT:
                currentTurnTarget.position = opponentPawnList[Index].transform.position;
                ToggleButtons(opponentPawnList[Index]);
                CameraManager.Instance.SetCameraPosition(opponentPawnList[Index].transform);
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
            case TurnState.TARGETING:
                StartTargeting();
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

    private void ToggleButtons(Pawn pawn)
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

    public void StartTargeting()
    {
        CameraManager.Instance.SetCameraPosition(null);
        turnState = TurnState.TARGETING;
    }

    private Vector3 SelectTarget(List<Pawn> pawns)
    {
        return pawns[selectionIndex].transform.position;
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
            GameObject spawnedButton = UIManager.Instance.CreateButton(action.name, action.StartTargeting, UIManager.Instance.ActionButtonTransform);
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
