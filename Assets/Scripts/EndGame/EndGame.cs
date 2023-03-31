using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    public int currentLevel = 1;

    private bool _endGame = false;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null && !_endGame)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;
        foreach (var item in endGameObjects)
        {
            item.SetActive(true);
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }

        SOPlayerData playerData = Player.Instance.playerData;
        playerData.currentHealth = 10;
        playerData.currentClothType = Cloth.ClothType.DEFAULT;
        CheckpointManager.Instance.lastCheckPointKey = 0;
        SaveManager.Instance.SaveLastLevel(currentLevel);
    }
}
