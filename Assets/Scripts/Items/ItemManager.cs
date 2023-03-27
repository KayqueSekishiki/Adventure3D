using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt specialBullets;

    public TextMeshProUGUI uiTextCoins;


    private void Start()
    {
        Reset();
    }


    private void Reset()
    {
        coins.value = 0;
        specialBullets.value = 0;
    }

    public void AddCoins(int amount)
    {
        coins.value += amount;
        UpdateTextCoins(coins.value);
    }

    public void UpdateTextCoins(int i)
    {
        if (i < 0)
        {
            uiTextCoins.text = "X " + i + " How did you do it?";
        }
        else if (i >= 0 && i < 10)
        {
            uiTextCoins.text = "x 0000" + i;
        }
        else if (i >= 10 && i < 100)
        {
            uiTextCoins.text = "x 000" + i;
        }
        else if (i >= 100 && i < 1000)
        {
            uiTextCoins.text = "x 00" + i;
        }
        else if (i >= 1000 && i < 10000)
        {
            uiTextCoins.text = "x 0" + i;
        }
        else if (i >= 10000 && i < 100000)
        {
            uiTextCoins.text = "x 0" + i;
        }
        else
        {
            uiTextCoins.text = "I'm Rich!";
        }
    }


    public void AddSpecialBullets(int amount)
    {
        specialBullets.value += amount;
    }
}
