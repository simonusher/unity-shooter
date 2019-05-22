using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RegisterManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Register()
    {
        string login = loginInputField.text;
        string password = passwordInputField.text;
        GameManager.manager.Register(login, password);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Login");
    }
}
