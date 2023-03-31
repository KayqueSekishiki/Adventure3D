using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public float currentLife { get { return _currentLife; } }


    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uiFillUpdaters;

    public float recoveryTimeDuration = 2f;
    public bool recoveryEnabled = false;

    private float recoveryCount;
    private bool iAmPlayer = false;

    public float damageMultiply = 1f;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        if (iAmPlayer)
        {
            _currentLife = SaveManager.Instance.Setup.currentPlayerHealth;
            UpdateUI();
        }
    }

    private void Update()
    {
        if (recoveryEnabled == true) recoveryCount += Time.deltaTime;
    }

    protected virtual void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    public void Damage(float f)
    {
        if (recoveryEnabled == true)
        {
            if (recoveryCount >= recoveryTimeDuration)
            {
                _currentLife -= f * damageMultiply;
                ShakeCamera.Instance.Shake();
                OnDamage?.Invoke(this);
                recoveryCount = 0;
            }
        }
        else
        {
            _currentLife -= f;
            ShakeCamera.Instance.Shake();
            OnDamage?.Invoke(this);
        }


        if (_currentLife <= 0)
        {
            Kill();
        }

        UpdateUI();
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }


    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject, 0f);

        OnKill?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiFillUpdaters != null)
        {
            uiFillUpdaters.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damageMultiply, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiply, duration));

    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1f;

    }


}
