using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;


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

        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        private void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, SaveManager.Instance.Setup.lifePacks);
        }

        private void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }

        public void AddByType(ItemType itemType, int amount)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0;
        }
    }


    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}