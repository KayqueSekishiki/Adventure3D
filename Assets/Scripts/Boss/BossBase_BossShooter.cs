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
        public float shootSpeed = 30f;


        public override void Update()
        {
            base.Update();
            shootPosition.DORotate(target.position, .2f);
        }

        public override void BossAttack()
        {
            int AttackMove = UnityEngine.Random.Range(1, 3);

            if (AttackMove == 1)
            {
                Debug.Log("Atirar");
                ShootAttack();
            }

            if (AttackMove == 2)
            {
                Debug.Log("Atropelar");
                TrampleAttack();
            }
        }




        public void ShootAttack()
        {
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
            var projectile = Instantiate(bossBulletPrefab, shootPosition);
            projectile.transform.SetPositionAndRotation(shootPosition.position, shootPosition.rotation);
            projectile.speed = shootSpeed;

            //projectile.transform.Translate(shootSpeed * Time.deltaTime * Vector3.forward);

        }

        public void TrampleAttack()
        {
            transform.DOScale(1.2f, timeBetweenAttacks / 2).SetLoops(2, LoopType.Yoyo);

            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            transform.DOMove(targetPosition, 3f);
        }
    }

}