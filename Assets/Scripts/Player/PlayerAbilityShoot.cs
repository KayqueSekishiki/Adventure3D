using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gunbases;
    public Transform gunPosition;

    private GunBase _currentGun;

    public FlashColor flashColor;

    protected override void Init()
    {
        base.Init();

        CreateGun(0);

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.ChangeToGun01.performed += ctx => CreateGun(0);
        inputs.Gameplay.ChangeToGun02.performed += ctx => CreateGun(1);
    }

    private void CreateGun(int gunIndex)
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject);
        _currentGun = Instantiate(gunbases[gunIndex], gunPosition);
        // _currentGun.transform.localPosition = _currentGun.transform.eulerAngles = Vector3.zero;
    }


    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}

