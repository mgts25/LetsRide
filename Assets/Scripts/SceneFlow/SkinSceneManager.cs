﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSceneManager : MonoBehaviour
{
    [SerializeField] private string prevSceneName = "12ShopScene";
    //[SerializeField] private string trickSceneName = "22TrickScene";
    //[SerializeField] private string equipSceneName = "22TrickScene";

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnTrickButton()
    {
        //SceneManager.LoadScene(trickSceneName);
    }

    public void OnEquipButton()
    {
        //SceneManager.LoadScene(equipSceneName);
    }
}