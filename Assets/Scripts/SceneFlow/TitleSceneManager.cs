using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    private string menuSceneName = "MenuScene";
    [SerializeField]
    private string loginSceneName = "LoginScene";

    public void OnGuestButton()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void OnLoginButton()
    {
        SceneManager.LoadScene(loginSceneName);
    }
}
