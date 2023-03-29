using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    [Header("Animation")]
    public float tweenDuration = .2f;
    public Ease ease = Ease.OutBack;
    public float collectTweenEndTime = .5f;
    public Vector2 randomRange = new(.5f, .5f);

    private List<GameObject> _items = new();



    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    [NaughtyAttributes.Button]
    private void CreateItems()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, tweenDuration).SetEase(ease).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach (var i in _items)
        {
            i.transform.DOMoveY(2f, collectTweenEndTime).SetRelative();
            i.transform.DOScale(0, collectTweenEndTime / 2).SetDelay(collectTweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN, 1);
        }
    }
}
