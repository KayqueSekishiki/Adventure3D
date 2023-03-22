using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunbase;

    protected override void Init()
    {
        base.Init();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.performed += ctx => CancelShoot();
    }


    private void StartShoot()
    {
        gunbase.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        gunbase.StopShoot();
    }
}

