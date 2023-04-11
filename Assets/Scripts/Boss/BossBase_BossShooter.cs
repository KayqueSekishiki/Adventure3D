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
        public float shootSpeed = 20f;

        public override void BossAttack()
        {
            int AttackMove = UnityEngine.Random.Range(1, 3);

            switch (AttackMove)
            {
                case 1:
                    ShootAttack();
                    break;

                case 2:
                    TrampleAttack();
                    break;

                default:
                    break;
            }
        }

        public void ShootAttack()
        {
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            var projectile = Instantiate(bossBulletPrefab, shootPosition);
            projectile.transform.position = shootPosition.position;
            Transform lookAtTarget = Player.Instance.transform;
            projectile.transform.LookAt(lookAtTarget);
            projectile.speed = shootSpeed;
        }

        public void TrampleAttack()
        {
            transform.DOScale(1.2f, timeBetweenAttacks / 2).SetLoops(2, LoopType.Yoyo);

            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.DOMove(targetPosition, .5f).SetEase(Ease.Linear);
        }
    }

}