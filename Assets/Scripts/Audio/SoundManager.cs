using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}
