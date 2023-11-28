using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Pawn enemy;

    private TurnState currentState;

    private float currentCooldown = 0f;
    private float maxCooldown = 5f;

    private Vector3 startPos;

    private bool actionStarted = false;
    private GameObject heroToAttack;
    private float animSpeed = 5f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case TurnState.PROCESSING:
                UpgradeProgressBar();
                break;
            case TurnState.CHOOSEACTION:
                ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case TurnState.WAITING:

                break;
            case TurnState.ACTION:
                StartCoroutine(TimeForAction());
                break;
            case TurnState.DEAD:
                break;
        }
    }

    private void UpgradeProgressBar()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }

    }

    private void ChooseAction()
    {
        HandleTurn newAttack = new HandleTurn();

        newAttack.instigatorName = enemy.PawnName;
        newAttack.type = "Enemy";
        newAttack.instigator = this.gameObject;
        newAttack.target = BattleStateMachine.Instance.HerosInBattle[(Random.Range(0, BattleStateMachine.Instance.HerosInBattle.Count))];
        BattleStateMachine.Instance.CollectActions(newAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        Vector3 heroPosition = new Vector3(heroToAttack.transform.position.x + 1.5f,
                                           heroToAttack.transform.position.y,
                                           heroToAttack.transform.position.z);

        while (MoveTowardsTarget(heroPosition))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 firstPosition = startPos;
        while (MoveTowardsTarget(firstPosition))
        {
            yield return null; 
        }



        actionStarted = false;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void SetAttackTarget(GameObject target)
    {
        heroToAttack = target;
    }

    public void SetState(TurnState state)
    {
        currentState = state;
    }

}
