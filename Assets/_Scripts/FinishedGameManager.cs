using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedGameManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Login");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
