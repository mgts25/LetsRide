using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "1TitleScene";
    [SerializeField] private string menuSceneName = "2MenuScene";
    [SerializeField] private string recoverSceneName = "5RecoverPasswordScene";
    [SerializeField] private string createAccountSceneName = "6CreateAccountScene";
    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnLogin()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void OnRecoverPassword()
    {
        SceneManager.LoadScene(recoverSceneName);
    }

    public void OnCreateAccount()
    {
        SceneManager.LoadScene(createAccountSceneName);
    }
}
