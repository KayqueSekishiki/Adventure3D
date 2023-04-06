using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        private bool attackMode = false;
        public GunBase gunBase;

   

        public override void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= lookRadius)
            {
                LookToPlayer();
                if (!attackMode)
                {
                    gunBase.StartShoot();
                    attackMode = true;
                }

            }
            else
            {
                gunBase.StopShoot();
                attackMode = false;
            }
        }
    }
}