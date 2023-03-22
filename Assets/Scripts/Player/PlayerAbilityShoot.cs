using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunbase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.performed += ctx => CancelShoot();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunbase, gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.eulerAngles = Vector3.zero;
    }


    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}

