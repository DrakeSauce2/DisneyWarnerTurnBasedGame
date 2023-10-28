using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/BaseAction")]
public class Action : ScriptableObject
{
    public GameObject Owner { get; private set; }
    public Pawn targetedPawn { get; private set; }

    [SerializeField] protected int actionUse;

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public virtual void AddActionToQueue()
    {
        Debug.Log($"Adding {this} To Queue!");

        ActionManager.Instance.EnqueueAction(this);
        TurnManager.Instance.Next();

        actionUse--;
    }

    public virtual void StartAction()
    {
        Debug.Log($"Action Initiated: {this}!");

    }

    #region Target Functions

    public void SetTarget(Pawn opponentPawn)
    {
        targetedPawn = opponentPawn;
    }

    public void RemoveTarget()
    {
        targetedPawn = null;
    }

    #endregion

}
