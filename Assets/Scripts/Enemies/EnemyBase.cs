using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
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

        public void OnDamage(float f)
        {
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
            Destroy(gameObject);
        }


        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }


        #endregion


        // debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }
    }

}