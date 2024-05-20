using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "12ShopScene";
    [SerializeField] private string skinSceneName = "13SkinScene";
    [SerializeField] private string gearSceneName = "14ClothScene";
    //[SerializeField] private string equipSceneName = "22TrickScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnSkinButton()
    {
        SceneManager.LoadScene(skinSceneName);
    }

    public void OnGearButton()
    {
        SceneManager.LoadScene(gearSceneName);
    }
}
