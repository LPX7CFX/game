using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public GameObject loginPanel;

    public void ConfirmName()
    {
        string username = nameInput.text.Trim();

        if (string.IsNullOrEmpty(username))
            return;

        if (SaveManager.Instance.HasSave(username))
        {
            // Existing user
            SaveManager.Instance.Load(username);
            Debug.Log("Loaded user: " + username);
        }
        else
        {
            // New user
            SaveManager.Instance.CreateNew(username);
            Debug.Log("Created new user: " + username);
        }

        loginPanel.SetActive(false);
    }
}
