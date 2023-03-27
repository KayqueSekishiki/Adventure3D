using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem EnemyParticleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;
        public bool followPlayer = false;
        public float lookRadius = 20f;
        public float speed = 1f;
        public int powerDamage = 2;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [HideInInspector] public Player player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            player = GameObject.FindObjectOfType<Player>();
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                LookToPlayer();
            }

            if (followPlayer)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= lookRadius)
                {
                    LookToPlayer();
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
                }
            }
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        public void Damage(float damage) // IDamageable
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir) // IDamageable
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (EnemyParticleSystem != null) EnemyParticleSystem.Emit(15);

            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)
            {
                p.healthBase.Damage(powerDamage);
                IAddExternalVelocity externalVelocity = collision.transform.GetComponent<IAddExternalVelocity>();
                if (externalVelocity != null)
                {
                    externalVelocity.AddExternalVelocity(2f, p.transform.position - transform.position);
                }
            }
        }

        protected virtual void LookToPlayer()
        {
            Vector3 lookPosition = player.transform.position;
            lookPosition.y = transform.position.y;
            transform.LookAt(lookPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }

        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

    }
}