using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrickCard : MonoBehaviour
{
    [SerializeField] private int trickIndex;
    [SerializeField] private Text trickName;
    [SerializeField] private Text status;
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private Color lockedColor;
    [SerializeField] private List<Sprite> backgroundSprites;
    public int price = 100;
    private bool isLocked = false;
    private bool isEquipped = false;

    private void Start()
    {
        //Equipped();
    }

    public void SetState(bool isLock = true, bool isEquipped = true)
    {
        if (isLock) Lock();

        if (isEquipped) Equipped();
    }

    public void Lock()
    {
        isEquipped = false;
        isLocked = true;
        status.enabled = true;
        status.text = "LOCKED";
        background.sprite = backgroundSprites[0];
        SetTrickColor(lockedColor);
    }

    public void Equipped()
    {
        isEquipped = true;
        isLocked = false;
        status.enabled = false;
        status.text = price.ToString();
        background.sprite = backgroundSprites[2];
        SetTrickColor(Color.white);
    }

    public void Normal()
    {
        isEquipped = false;
        isLocked = false;
        status.enabled = true;
        status.text = price.ToString();
        background.sprite = backgroundSprites[1];
        SetTrickColor(Color.white);
    }

    private void SetTrickColor(Color color)
    {
        background.color = color;
        icon.color = color;
    }

    public void OnClick()
    {
        TrickSceneManager.instance.BuyTrick(trickIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Equipped();
        if (Input.GetKeyDown(KeyCode.W))
            Lock();
        if (Input.GetKeyDown(KeyCode.E))
            Normal();
    }
}
