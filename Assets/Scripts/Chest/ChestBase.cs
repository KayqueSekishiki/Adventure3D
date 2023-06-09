using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{

    public KeyCode keyCode = KeyCode.E;
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;

    [Header("SFX")]
    public AudioSource selfAudioSource;
    public SFXType sfxType;

    private float _startScale;
    private bool _chestOpened = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            HideNotification();
        }

        _startScale = notification.transform.localScale.x;

    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }


    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;
        animator.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification();
        selfAudioSource.Stop();
        PlaySFX();
        Invoke(nameof(ShowItem), 2f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.DOScale(_startScale, tweenDuration).SetEase(tweenEase);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.transform.DOScale(0, tweenDuration).SetEase(tweenEase);
        Invoke(nameof(OnHideNotification), tweenDuration);
    }

    private void OnHideNotification()
    {
        notification.SetActive(false);
    }

    private void PlaySFX()
    {
        SFXPool.Instance.PlaySFX(sfxType);
    }
}
