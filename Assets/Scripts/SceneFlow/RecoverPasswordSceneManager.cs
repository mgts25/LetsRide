using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecoverPasswordSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "4LoginScene";
    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

}
