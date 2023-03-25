using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int amountPerShoot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        int mult = 0;
     

        for (int i = 0; i < amountPerShoot; i++)
        {
            if (i % 2 == 0)
            {
                mult++;
            }

            var projectile = Instantiate(prefabProjectile, positionShoot);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = (i % 2 == 0 ? angle : -angle) * mult * Vector3.up + Vector3.zero;
            projectile.speed = speed;
            projectile.transform.parent = null;
        }

        var projectileMid = Instantiate(prefabProjectile, positionShoot);
        projectileMid.transform.localPosition = Vector3.zero;
        projectileMid.speed = speed;
        projectileMid.transform.parent = null;

    }



}
