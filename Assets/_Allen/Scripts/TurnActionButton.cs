using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonActionName;
    private Button button;

    private void Init(UnityEngine.Events.UnityAction call, string actionName)
    {
        button = GetComponent<Button>();

        buttonActionName.text = actionName;

        button.onClick.AddListener(call);
    }

}
