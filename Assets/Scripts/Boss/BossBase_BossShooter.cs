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
        public ProjectileBase bossBulletPrefab;
        public Transform shootPosition;
        public float shootSpeed = 10f; 

        public override void BossAttack()
        {
            int AttackMove = UnityEngine.Random.Range(1, 3);

            if (AttackMove == 1)
            {
                Debug.Log("Atirar");
                ShootAttack();
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            }

            if (AttackMove == 2)
            {
                Debug.Log("Atropelar");
                TrampleAttack();
                transform.DOScale(1.2f, timeBetweenAttacks).SetLoops(2, LoopType.Yoyo);

            }
        }


        public void ShootAttack()
        {
            var projectile = Instantiate(bossBulletPrefab, shootPosition);
            projectile.transform.SetPositionAndRotation(shootPosition.position, shootPosition.rotation);
            projectile.speed = shootSpeed;

        }

        public void TrampleAttack()
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * speed * 3);
        }
    }

}