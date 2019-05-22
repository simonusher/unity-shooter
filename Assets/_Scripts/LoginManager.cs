using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LogIn()
    {
        string login = loginInputField.text;
        string password = passwordInputField.text;
        GameManager.manager.LogIn(login, password);
    }

    public void Register()
    {
        SceneManager.LoadScene("Register");
    }
}
