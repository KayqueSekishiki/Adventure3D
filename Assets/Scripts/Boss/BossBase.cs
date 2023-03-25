using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        private StateMachine<BossAction> stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Walk")]
        public float speed = 5f;
        public List<Transform> waypoints;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
        }

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke();
        }

        #endregion


        #region ATTACK  
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(StartAttackCoroutine(endCallback));
        }

        IEnumerator StartAttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }

        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        public void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        public void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        public void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchStates(state, this);
        }

        #endregion
    }

}