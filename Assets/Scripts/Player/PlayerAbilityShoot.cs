using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gunbases;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.performed += ctx => CancelShoot();
        inputs.Gameplay.ChangeToGun01.performed += ctx => ChangeGun(0);
        inputs.Gameplay.ChangeToGun02.performed += ctx => ChangeGun(1);

    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunbases[0], gunPosition);
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

    private void ChangeGun(int gunIndex)
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject);
        _currentGun = Instantiate(gunbases[gunIndex], gunPosition);
    }
}

