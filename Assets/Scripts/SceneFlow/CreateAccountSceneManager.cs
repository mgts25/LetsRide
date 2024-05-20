using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateAccountSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "4LoginScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnRegister()
    {
        SceneManager.LoadScene(prevSceneName);
    }

}
