using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compateTag = "Player";
        public float timeToHide = 2f;
        public Collider myCollider;
        public GameObject grafic;
        public ParticleSystem myParticleSystem;
        public Transform parentVFX;
        public Transform parentSFX;

        [Header("Sounds")]
        public AudioSource audioSource;


        private void Awake()
        {
            if (myParticleSystem != null)
            {
                myParticleSystem.transform.SetParent(parentVFX);
            }
            if (audioSource != null)
            {
                audioSource.transform.SetParent(parentSFX);
            }

            if (myCollider == null)
            {
                myCollider = GetComponent<Collider>();
            }
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compateTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            PlaySFX();
            myCollider.enabled = false;
            grafic.SetActive(false);
            Invoke(nameof(HideItems), timeToHide);
            OnCollect();
        }

        protected virtual void HideItems()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        protected virtual void OnCollect()
        {
            if (myParticleSystem != null)
            {
                myParticleSystem.transform.SetParent(null);
                myParticleSystem.Play();
            }

            if (audioSource != null)
            {
                audioSource.transform.SetParent(null);
                audioSource.Play();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.PlaySFX(sfxType);
        }
    }

}