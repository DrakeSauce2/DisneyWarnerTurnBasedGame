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

    public void StartTargeting()
    {
        CameraManager.Instance.SetCameraPosition(null);

        TurnManager.Instance.StartTargeting();
    }

    public virtual void AddActionToQueue()
    {
        Debug.Log($"Adding {this} To Queue!");

        // Select Target Before Doing Next!

        ActionManager.Instance.EnqueueAction(this);

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

        AddActionToQueue();
    }

    public void RemoveTarget()
    {
        targetedPawn = null;
    }

    #endregion

}
