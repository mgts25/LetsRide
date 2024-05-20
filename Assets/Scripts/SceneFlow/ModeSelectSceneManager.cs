using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "2MenuScene";
    [SerializeField] private string choosePathSceneName = "9ChoosePathScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnPipe()
    {
        SceneManager.LoadScene(choosePathSceneName);
    }

    public void OnGrind()
    {
        SceneManager.LoadScene(choosePathSceneName);
    }
}
