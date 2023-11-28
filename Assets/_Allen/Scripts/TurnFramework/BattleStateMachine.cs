using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    public static BattleStateMachine Instance;

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    private PerformAction battleStates;

    public List<HandleTurn> performList = new List<HandleTurn>();

    [SerializeField] private List<GameObject> herosInBattle = new List<GameObject>();
    [SerializeField] private List<GameObject> enemiesInBattle = new List<GameObject>();
    public List<GameObject> HerosInBattle { get { return herosInBattle; } }
    public List<GameObject> EnemiesInBattle { get { return enemiesInBattle; } }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        battleStates = PerformAction.WAIT;
    }

    private void Update()
    {
        switch (battleStates)
        {
            case PerformAction.WAIT:
                if (performList.Count > 0)
                {
                    battleStates |= PerformAction.TAKEACTION;
                }

                break;
            case PerformAction.TAKEACTION:
                GameObject performer = GameObject.Find(performList[0].instigatorName);
                if (performList[0].type == "Enemy")
                {
                    EnemyStateMachine esm = performer.GetComponent<EnemyStateMachine>();
                    esm.SetAttackTarget(performList[0].target);
                    esm.SetState(TurnState.ACTION);
                }

                if (performList[0].type == "Hero")
                {

                }
                battleStates = PerformAction.PERFORMACTION;
                break;
            case PerformAction.PERFORMACTION:
                break;
        }

    }

    public void CollectActions(HandleTurn input)
    {
        performList.Add(input);
    }

}
