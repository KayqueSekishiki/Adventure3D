using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uiFillUpdaters;
    public bool updateUIs = true;
    public float maxShoot = 5;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;

    private void Awake()
    {
        if (updateUIs)
        {
            GetAllUIs();
        }
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0f;

        while (time < timeToRecharge)
        {
            time += Time.deltaTime;

            var gunUI = uiFillUpdaters.Find(i => i.name == "GunUI");
            if (gunUI != null)
            {
                gunUI.UpdateValue(time / timeToRecharge);
            }
            //  uiFillUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        if (uiFillUpdaters != null)
        {
            var gunUI = uiFillUpdaters.Find(i => i.name == "GunUI");
            if (gunUI != null)
            {
                gunUI.UpdateValue(maxShoot, _currentShoots);
            }
            //uiFillUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
        }
    }

    private void GetAllUIs()
    {
        uiFillUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
    }
}
