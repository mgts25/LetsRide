using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int Coins { get { return coins; } }
    private int coins;
    [SerializeField] string coinSaveKey = "RemainCoins";
    [SerializeField] int defaultCoinCount = 2000;

    public bool CostCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt(coinSaveKey, coins);
            return true;
        }
        return false;
    }

    private void Awake()
    {
        coins = PlayerPrefs.GetInt(coinSaveKey, defaultCoinCount);
    }
}
