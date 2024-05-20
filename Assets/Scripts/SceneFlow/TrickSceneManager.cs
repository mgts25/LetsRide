using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrickSceneManager : RootMotion.Singleton<TrickSceneManager>
{
    [SerializeField] private string prevSceneName = "2MenuScene";
    [SerializeField] private string trickSceneName = "22TrickScene";
    //[SerializeField] private string equipSceneName = "22TrickScene";
    [SerializeField] private List<TrickCard> cards = new List<TrickCard>();
    private int trickType = 0;
    private TrickManager trickManager;

    public void OnBackButton()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void OnTrickButton(int type)
    {
        //SceneManager.LoadScene(trickSceneName);
        trickType = type;
        TrickManager.Instance.SelectTrickType(type);
        ReloadTricks();
    }

    public void ReloadTricks()
    {
        // TODO
    }

    public void OnEquipButton()
    {
        //SceneManager.LoadScene(equipSceneName);
    }

    public void BuyTrick(int index)
    {
        /// TODO : block logic rebuy
        if (!TrickManager.Instance.IsTrickEquipped(index - 1) && CoinManager.Instance.CostCoins(100))
        {
            cards[index - 1].Equipped();
            TrickManager.Instance.EquipTrick(trickType, index - 1);
        }
    }
}
