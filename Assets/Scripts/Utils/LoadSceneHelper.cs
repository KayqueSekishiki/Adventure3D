using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ebac.Core.Singleton;

public class LoadSceneHelper : Singleton<LoadSceneHelper>
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









