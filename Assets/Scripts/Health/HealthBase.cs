using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uiFillUpdaters;



    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);

    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }


    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject, 3f);

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
}
