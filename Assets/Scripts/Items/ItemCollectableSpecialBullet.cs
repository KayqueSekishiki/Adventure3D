using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableSpecialBullet : ItemCollectableBase
{
    public int specialBulletValue;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddSpecialBullets(specialBulletValue);
    }
}
