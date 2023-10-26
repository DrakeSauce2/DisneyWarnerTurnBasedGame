using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/BaseAction")]
public class Action : ScriptableObject
{
    public GameObject Owner { get; private set; }
    public Pawn targetedPawn { get; private set; }

    [SerializeField] private int actionUse;

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public virtual void StartAction()
    {
        Debug.Log($"Action Initiate By: {this}");

        ActionManager.Instance.EnqueueAction(this);
        TurnManager.Instance.Next();

        actionUse--;
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
