using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (CheckpointManager.Instance.lastCheckPointKey == 0)
            {
                LoadSceneHelper.Instance.LoadLevel();
            }
            else
            {
                Player.Instance.Respawn();
            }
        }
    }
}
