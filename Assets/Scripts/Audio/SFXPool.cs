using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourcesList;

    public int poolSize = 10;

    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourcesList = new();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourcesList.Add(go.AddComponent<AudioSource>());
    }

    public void PlaySFX(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourcesList[_index].clip = sfx.audioClip;
        _audioSourcesList[_index].Play();
        _index++;

        if (_index >= _audioSourcesList.Count) _index = 0;
    }

}
