using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ebac.Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioMixer masterMixer;
    private bool soundMuted = false;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public void PlaySFXByType(SFXType sfxType)
    {
        var sfx = GetSFXByType(sfxType);
        musicSource.clip = sfx.audioClip;
        musicSource.Play();
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }


    public void MuteUnmuteSound()
    {
        soundMuted = !soundMuted;
        UIManager.Instance.ChangeButtonMuteIcon();
        if (soundMuted)
        {
            masterMixer.SetFloat("VolumeMaster", -80f);
        }
        else
        {
            masterMixer.SetFloat("VolumeMaster", 0f);
        }
    }
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}


public enum SFXType
{
    NONE,
    SFX_COINS,
    SFX_LIFEPACK,
    SFX_JUMP,
    SFX_OPENCHEST
}


[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}




