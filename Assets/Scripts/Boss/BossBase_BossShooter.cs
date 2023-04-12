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


        public override IEnumerator StartAttackCoroutine(Action endCallback)
        {
            int AttackMove = UnityEngine.Random.Range(1, 3);

            switch (AttackMove)
            {
                case 1:
                    int attacks = 0;
                    int randmAttacks = UnityEngine.Random.Range(1, maxAttackAmount + 1);
                    while (attacks < randmAttacks)
                    {
                        attacks++;
                        Debug.Log("Atirei no Jogador");
                        ShootAttack();
                        yield return new WaitForSeconds(timeBetweenAttacks);
                    }
                    break;

                case 2:
                    Debug.Log("Atropelar");
                    ChargeBehaviour();
                    charging = false;
                    break;

                default:
                    break;
            }

            endCallback?.Invoke();
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
    }

}