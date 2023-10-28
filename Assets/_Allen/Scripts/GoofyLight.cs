using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/GoofyLightAction")]
public class GoofyLight : Action
{
    public override void AddActionToQueue()
    {
        Debug.Log($"Action Initiate By: {this}");

        ActionManager.Instance.EnqueueAction(this);
        TurnManager.Instance.Next();

        actionUse--;
    }
}
