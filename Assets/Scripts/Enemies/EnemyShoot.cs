using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            //gunBase.StartShoot();
        }


        public override void Update()
        {
            base.Update();
            gunBase.StartShoot();
        }
    }
}