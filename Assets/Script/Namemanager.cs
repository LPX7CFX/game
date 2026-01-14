using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LoginManager : MonoBehaviour
{
    public static string CurrentUser;

    public TMP_InputField nameInput;
    public GameObject loginPanel;
    public GameObject startScene;
    public Button confirmButton;

    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmName);
    }

    void ConfirmName()
    {
        string username = nameInput.text.Trim();

        if (string.IsNullOrEmpty(username))
            return;

        CurrentUser = username;

        loginPanel.SetActive(false);
        startScene.SetActive(true);
    }
}
