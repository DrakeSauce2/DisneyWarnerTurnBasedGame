using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{
    public GameObject Owner { get; private set; }

    [SerializeField] private int actionUse;

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public virtual void StartAction()
    {

    }

}
