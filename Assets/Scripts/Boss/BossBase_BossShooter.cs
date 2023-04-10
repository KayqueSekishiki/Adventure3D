using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public class BossBase_BossShooter : BossBase
    {
        public GameObject bossBulletPrefab;

        public override void BossAttack()
        {
            int AttackMove = UnityEngine.Random.Range(1, 3);

            if (AttackMove == 1)
            {
                Debug.Log("Atirar");
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            }

            if (AttackMove == 2)
            {
                Debug.Log("Atropelar");
            }
        }
    }

}