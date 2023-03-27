using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableLifePack : ItemCollectableBase
{
    public int lifePackValue;

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.LIFE_PACK, lifePackValue);
    }
}
