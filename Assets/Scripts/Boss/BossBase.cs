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
        ATTACK,
        DEATH
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
        public int maxAttackAmount = 5;
        public float timeBetweenAttacks = .5f;

        [Header("References")]
        public HealthBase healthBase;

        [Header("SETUP")]
        public float lookRadius;
        public float attackRadius;
        public Transform target;

        public GameObject bossCamera;

        public bool bossSpawned = false;
        private bool _attackMode = false;



        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            Init();
            OnValidate();
            if (healthBase != null)
            {
                healthBase.OnKill += OnBossKill;
            }
            target = FindObjectOfType<Player>().transform;
        }

        private void Update()
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (!bossSpawned)
            {
                if (distance <= lookRadius)
                {
                    SwitchState(BossAction.INIT);
                    bossSpawned = true;
                }
            }

            if (distance <= lookRadius)
            {
                Vector3 lookPosition = target.transform.position;
                lookPosition.y = transform.position.y;
                transform.LookAt(lookPosition);
                bossCamera.SetActive(true);

                if (distance <= attackRadius && !_attackMode)
                {
                    SwitchState(BossAction.WALK);
                    _attackMode = true;
                }
                else if (distance > attackRadius && _attackMode)
                {
                    SwitchState(BossAction.INIT);
                    _attackMode = false;
                }
            }
            else
            {
                bossCamera.SetActive(false);
            }

        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            bossCamera.SetActive(false);
            SwitchState(BossAction.DEATH);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
            Gizmos.DrawWireSphere(transform.position, attackRadius);
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

        public virtual IEnumerator StartAttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            int randmAttacks = UnityEngine.Random.Range(1, maxAttackAmount + 1);
            while (attacks < randmAttacks)
            {
                attacks++;
                BossAttack();
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }

        public virtual void BossAttack()
        {
            Debug.Log("Ataquei o Jogador!");
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
        }

        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(1, startAnimationDuration).SetEase(startAnimationEase);
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