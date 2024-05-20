using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private string playSceneName = "PlayScene";
    [SerializeField]
    private string lobbySceneName = "LobbyScene";
    [SerializeField]
    private string statsSceneName = "StatsScene";
    [SerializeField]
    private string shopSceneName = "ShopScene";

    public void OnPlayButton()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void OnLobbyButton()
    {
        SceneManager.LoadScene(lobbySceneName);
    }

    public void OnStatsButton()
    {
        SceneManager.LoadScene(statsSceneName);
    }

    public void OnShopButton()
    {
        SceneManager.LoadScene(shopSceneName);
    }
}
