using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Actions/Moves/Attacks")]
    [SerializeField] private List<Action> actionsList = new List<Action>();
    public List<Action> ActionsList { get { return actionsList; } }

    [Header("Health Bar Component")]
    [SerializeField] private ValueGauge healthBarPrefab;   
    private HealthComponent healthComponent;
    private ValueGauge healthBar;

    // Spawned Action Buttons
    private List<GameObject> turnActionButtons = new List<GameObject>();

    private void Awake()
    {
        foreach (Action action in actionsList)
        {
            action.Init(gameObject);
        }

        //InitializeHealthComponent();
    }
    // Make Health bar smaller using distance to camera
    #region Health Component Functions

    private void InitializeHealthComponent()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.onTakenDamage += TookDamage;
        healthComponent.onHealthEmpty += StartDeath;
        healthComponent.onHealthChanged += HealthChanged;

        healthBar = Instantiate(healthBarPrefab);
    }

    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        healthBar.SetValue(currentHealth, maxHealth);
    }

    private void StartDeath(float delta, float maxHealth)
    {
        Debug.Log("Death");
    }

    private void TookDamage(float currentHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"Took Damage {delta}, now health is {currentHealth}/{maxHealth}");
    }

    #endregion

    #region Action Button Functions

    public void SetActionButtons(List<GameObject> actionButtons)
    {
        turnActionButtons = actionButtons;
    }

    public void ToggleActionButtons(bool state)
    {
        foreach (GameObject actionButton in turnActionButtons)
        {
            actionButton.SetActive(state);
        }
    }

    #endregion

}
