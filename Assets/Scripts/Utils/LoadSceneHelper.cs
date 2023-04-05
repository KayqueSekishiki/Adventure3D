using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void LoadLevel()
    {
        int lastLevel = SaveManager.Instance.lastLevel;
        SceneManager.LoadScene(lastLevel + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}









