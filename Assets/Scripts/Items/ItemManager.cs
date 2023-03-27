using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;


namespace Items
{

    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;
        public TextMeshProUGUI uiTextCoins;

        private void Start()
        {
            Reset();
        }


        private void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public void AddByType(ItemType itemType, int amount)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value += amount;
            if (item.itemType == ItemType.COIN) UpdateTextCoins(item.soInt.value);
        }

        public void RemoveByType(ItemType itemType, int amount)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            if (item.soInt.value < 0)
            {
                item.soInt.value -= amount;
            }
            if (item.itemType == ItemType.COIN) UpdateTextCoins(item.soInt.value);
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
    }


    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
    }
}