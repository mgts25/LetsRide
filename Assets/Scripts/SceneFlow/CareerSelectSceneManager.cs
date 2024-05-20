using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CareerSelectSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "9ChoosePathScene";
    [SerializeField] private string playSceneName = "PlayScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(playSceneName);
    }

}
