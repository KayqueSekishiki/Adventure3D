using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;
    public TextMeshProUGUI endGameCounterText;

    public int currentLevel = 1;
    private bool _endGame = false;

    private float _countToReturn = 10f;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void Update()
    {
        if (_endGame)
        {
            _countToReturn -= Time.deltaTime;
            endGameCounterText.text = _countToReturn.ToString("00.0");

            if (_countToReturn <= 0)
            {
                endGameCounterText.text = "Returning to Menu!";
                Invoke(nameof(ReturnToMenu), 1f);
            }
        }
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

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
