using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{
    LoadSceneHelper loadSceneHelper;
    private void Start()
    {
        FindObjectOfType<LoadSceneHelper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (CheckpointManager.Instance.lastCheckPointKey == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Player.Instance.Respawn();
            }
        }
    }
}
