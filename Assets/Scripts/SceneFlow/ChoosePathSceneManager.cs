using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePathSceneManager : MonoBehaviour
{

    [SerializeField] private string prevSceneName = "8ModeSelectScene";
    [SerializeField] private string chooseCareerSceneName = "17CareerScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnCareer()
    {
        SceneManager.LoadScene(chooseCareerSceneName);
    }

    public void OnRanked()
    {
        SceneManager.LoadScene(chooseCareerSceneName);
    }
}
