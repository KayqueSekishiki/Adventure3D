using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 30f;

    private Coroutine _currentCoroutine;

    public bool waitingForShoot = false;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.SetPositionAndRotation(positionShoot.position, positionShoot.rotation);
        projectile.speed = speed;
    }

    public void StartShoot()
    {
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }
    public void StopShoot()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            StartWaitForShoot();
        }
    }

    private void StartWaitForShoot()
    {
        waitingForShoot = true;
        StartCoroutine(WaitForShoot());
    }

    IEnumerator WaitForShoot()
    {
        float time = 0;

        while (time < timeBetweenShoot)
        {
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        waitingForShoot = false;
    }
}
