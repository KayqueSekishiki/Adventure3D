using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckPointKey = 0;
    public List<CheckpointBase> checkpoints;

    private void Start()
    {
        LoadCurrentCheckpointFromSave();
    }

    private void LoadCurrentCheckpointFromSave()
    {
        lastCheckPointKey = SaveManager.Instance.Setup.currentCheckpoint;
        HasCheckpoint();
    }

    public bool HasCheckpoint()
    {
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
            SaveManager.Instance.SaveLastCheckpoint();
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}
