using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    [SerializeField] private Queue<Action> actionsList = new Queue<Action>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public void EnqueueAction(Action actionToAdd)
    {
        actionsList.Enqueue(actionToAdd);
        Debug.Log($"Queue Size: {actionsList.Count}");
    }

    public void InitiateActionSequence()
    {
        Action currentAction = actionsList.Dequeue();

        currentAction.StartAction();
    }

}
