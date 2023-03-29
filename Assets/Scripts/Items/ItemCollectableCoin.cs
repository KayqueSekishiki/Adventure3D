using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableCoin : ItemCollectableBase
{
    public int coinValue;
    public Collider otherCollider;

    protected override void OnCollect()
    {
        base.OnCollect();
        if(otherCollider != null) otherCollider.enabled = false;
        ItemManager.Instance.AddByType(ItemType.COIN, coinValue);
    }
}
