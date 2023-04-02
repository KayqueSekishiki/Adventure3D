using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ebac.Core.Singleton;

public class UIManager : Singleton<UIManager>
{
    public Image MuteSoundImageButton;

    public Sprite SoundOn;
    public Sprite SoundOff;


    public void ChangeButtonMuteIcon()
    {
        MuteSoundImageButton.sprite = MuteSoundImageButton.sprite == SoundOn ? SoundOff : SoundOn;
    }
}
