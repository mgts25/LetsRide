using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrickManager : Singleton<TrickManager>
{
    [SerializeField] List<TrickCard> trickCards = new List<TrickCard>();
    //public List<int> trick1List = new List<int>();
    public List<int> trick1AllowList = new List<int>();
    public List<int> trick1PriceList = new List<int>();
    public List<string> trick1NameList = new List<string>();

    //public List<int> trick2List = new List<int>();
    public List<int> trick2AllowList = new List<int>();
    public List<int> trick2PriceList = new List<int>();
    public List<string> trick2NameList = new List<string>();

    //public List<int> trick3List = new List<int>();
    public List<int> trick3AllowList = new List<int>();
    public List<int> trick3PriceList = new List<int>();
    public List<string> trick3NameList = new List<string>();

    //public List<int> trick4List = new List<int>();
    public List<int> trick4AllowList = new List<int>();
    public List<int> trick4PriceList = new List<int>();
    public List<string> trick4NameList = new List<string>();

    List<List<string>> trickNames = new List<List<string>>();

    public List<List<int>> equipedTrickIndex = new List<List<int>>(4);

    private string equipedTrick1Key = "equipTrick1";
    private string equipedTrick2Key = "equipTrick2";
    private string equipedTrick3Key = "equipTrick3";
    private string equipedTrick4Key = "equipTrick4";

    private int currentTrickType = 0;

    public bool IsTrickEquipped(int trickInd) => equipedTrickIndex[currentTrickType].Contains(trickInd);

    private void Awake()
    {
        equipedTrickIndex.Add(new List<int>());
        equipedTrickIndex.Add(new List<int>());
        equipedTrickIndex.Add(new List<int>());
        equipedTrickIndex.Add(new List<int>());
        trickNames.Add(trick1NameList);
        trickNames.Add(trick2NameList);
        trickNames.Add(trick3NameList);
        trickNames.Add(trick4NameList);

        //GetEquippedTricks(equipedTrick1Key, equipedTrickIndex[0]);
        //GetEquippedTricks(equipedTrick2Key, equipedTrickIndex[1]);
        //GetEquippedTricks(equipedTrick3Key, equipedTrickIndex[2]);
        //GetEquippedTricks(equipedTrick4Key, equipedTrickIndex[3]);

        var equipStr = PlayerPrefs.GetString(equipedTrick1Key, "");
        if (equipStr != "") equipedTrickIndex[0] = equipStr.Split(',').ToList().Select(int.Parse).ToList();

        equipStr = PlayerPrefs.GetString(equipedTrick2Key, "");
        if (equipStr != "") equipedTrickIndex[1] = equipStr.Split(',').ToList().Select(int.Parse).ToList();

        equipStr = PlayerPrefs.GetString(equipedTrick3Key, "");
        if (equipStr != "") equipedTrickIndex[2] = equipStr.Split(',').ToList().Select(int.Parse).ToList();

        equipStr = PlayerPrefs.GetString(equipedTrick4Key, "");
        if (equipStr != "") equipedTrickIndex[3] = equipStr.Split(',').ToList().Select(int.Parse).ToList();
        ShowTrickCards(0);
    }

    private void GetEquippedTricks(string key, List<int> equippedTricks)
    {
        var equipStr = PlayerPrefs.GetString(key, "0");
        if (equipStr == "0")
            equippedTricks.Add(0);
        else
            equippedTricks = equipStr.Split(',').ToList().Select(int.Parse).ToList();
    }

    public void ShowTrickCards(int trickType)
    {
        equipedTrickIndex[trickType].ForEach(i => Debug.Log(i.ToString()));
        for (int i = 0; i < 6; i++)
        {
            if (equipedTrickIndex[trickType].Contains(i))
                trickCards[i].Equipped();
            else
                trickCards[i].Normal();
        }
    }

    private string GetSaveKeyOfEquip(int index)
    {
        switch (index)
        {
            case 0:
                return equipedTrick1Key;
            case 1:
                return equipedTrick2Key;
            case 2:
                return equipedTrick3Key;
            default:
                return equipedTrick4Key;
        }
    }

    public void SelectTrickType(int type)
    {
        currentTrickType = type;
        ShowTrickCards(currentTrickType);
    }

    private void SetEquippedTricks(string key, List<int> equippedTricks)
    {
        string saveStr = "";
        equippedTricks.ForEach(i => saveStr += i.ToString() + (equippedTricks.IndexOf(i) == equippedTricks.Count - 1 ? "" : ","));
        Debug.Log(saveStr);
        PlayerPrefs.SetString(key, saveStr);
    }

    public void BuyTrick(int trickType, int trickIndex)
    {
        switch (trickType)
        {
            case 0:
                trick1AllowList.Add(trickIndex);
                break;
            case 1:
                trick2AllowList.Add(trickIndex);
                break;
            case 2:
                trick3AllowList.Add(trickIndex);
                break;
            case 3:
                trick4AllowList.Add(trickIndex);
                break;
        }
    }

    public void EquipTrick(int trickType, int trickIndex)
    {
        equipedTrickIndex[trickType].Add(trickIndex);
        SetEquippedTricks(GetSaveKeyOfEquip(trickType), equipedTrickIndex[trickType]);
        //switch (trickType)
        //{
        //    case 0:
        //        equipedTrick1Index.Add(trickIndex);
        //        SetEquippedTricks(GetSaveKeyOfEquip(0), equipedTrick1Index);
        //        break;
        //    case 1:
        //        equipedTrick2Index.Add(trickIndex);
        //        SetEquippedTricks(GetSaveKeyOfEquip(1), equipedTrick2Index);
        //        break;
        //    case 2:
        //        equipedTrick3Index.Add(trickIndex);
        //        SetEquippedTricks(GetSaveKeyOfEquip(2), equipedTrick3Index);
        //        break;
        //    case 3:
        //        equipedTrick4Index.Add(trickIndex);
        //        SetEquippedTricks(GetSaveKeyOfEquip(3), equipedTrick4Index);
        //        break;
        //}
    }
}
