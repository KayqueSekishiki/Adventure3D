using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{

    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public ClothChanger clothChanger;

    public Action<SaveSetup> FileLoaded;
    public SaveSetup Setup { get { return _saveSetup; } }


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(LoadFile), .1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new();
        _saveSetup.lastLevel = 0;
        _saveSetup.coins = 0;
        _saveSetup.lifePacks = 0;
        _saveSetup.currentCheckpoint = 0;
        _saveSetup.currentPlayerHealth = Player.Instance.healthBase.startLife;
        _saveSetup.currentCloth = (Texture2D)clothChanger.mesh.materials[0].GetTexture("_EmissionMap");
    }


    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        SaveLastCheckpoint();
        SaveCurrentPlayerHealth();
        SaveCurrentPlayerCloth();
        Save();
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.lifePacks = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveLastCheckpoint()
    {
        _saveSetup.currentCheckpoint = CheckpointManager.Instance.lastCheckPointKey;
        Save();
    }

    public void SaveCurrentPlayerHealth()
    {
        _saveSetup.currentPlayerHealth = Player.Instance.healthBase.currentLife;
        Save();
    }

    public void SaveCurrentPlayerCloth()
    {
        _saveSetup.currentCloth = (Texture2D)clothChanger.mesh.materials[0].GetTexture("_EmissionMap");
        Save();
    }

    #endregion
    private void SaveFile(string json)
    {
        string fileLoaded = "";
        if (File.Exists(_path)) fileLoaded = File.ReadAllText(_path);
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void LoadFile()
    {
        string fileLoaded = "";
        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int coins;
    public int lifePacks;
    public int currentCheckpoint;
    public float currentPlayerHealth;
    public Texture2D currentCloth;
}
