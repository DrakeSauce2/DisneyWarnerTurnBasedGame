using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform actionButtonAttachTransform;
    public Transform ActionButtonAttachTransform { get { return actionButtonAttachTransform; } }

    //private UIManager UiManager;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
       //UiManager = gameObject.AddComponent<UIManager>();
    }

}
