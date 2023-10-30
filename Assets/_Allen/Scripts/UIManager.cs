using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private RectTransform generalButtonTransform;
    public RectTransform GeneralButtonTransform { get { return generalButtonTransform; } }

    [SerializeField] private RectTransform actionButtonTransform;
    public RectTransform ActionButtonTransform { get { return actionButtonTransform; } }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        CreateButton("Fight", Fight, generalButtonTransform);
        CreateButton("Defend", Defend, generalButtonTransform);
        CreateButton("Menu", Menu, generalButtonTransform);
    }

    #region Button Functions

    private void Fight()
    {
        actionButtonTransform.gameObject.SetActive(true);
    }

    private void Defend()
    {

    }

    private void Menu()
    {

    }

    #endregion

    #region Button Creation

    public GameObject CreateButton(string text, UnityEngine.Events.UnityAction call, Transform buttonTransform)
    {
        GameObject buttonGameObject = Instantiate(new GameObject($"{text} Button"), buttonTransform);
        
        buttonGameObject.AddComponent<Button>();
        buttonGameObject.AddComponent<Image>();

        Button button = buttonGameObject.GetComponent<Button>();
        button.onClick.AddListener(call);

        button.navigation.Equals(Navigation.Mode.None);

        CreateButtonText(text, buttonGameObject.transform);

        return buttonGameObject;
    }

    private GameObject CreateButtonText(string text, Transform buttonTransform)
    {
        GameObject buttonTextObject = new GameObject(text);

        TextMeshProUGUI buttonText = buttonTextObject.AddComponent<TextMeshProUGUI>();
        buttonText.text = text;
        buttonText.color = Color.black;
        buttonText.horizontalAlignment = HorizontalAlignmentOptions.Center;
        buttonText.verticalAlignment = VerticalAlignmentOptions.Middle;


        return Instantiate(buttonTextObject, buttonTransform);
    }

    #endregion

}
