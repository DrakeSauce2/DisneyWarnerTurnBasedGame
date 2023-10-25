using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    private Queue<Action> actionsList;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public void EnqueueAction(Action actionToAdd)
    {
        actionsList.Enqueue(actionToAdd);
    }

    public void InitiateActionSequence()
    {
        Action currentAction = actionsList.Dequeue();

        currentAction.StartAction();
    }

}
