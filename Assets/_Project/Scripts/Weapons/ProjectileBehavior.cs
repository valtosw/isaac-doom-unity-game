using Assets._Project.Scripts.Core;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileBehavior : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 5f;

        private float _damageAmount;
        private bool _hasHit;

        public void Initialize(float damage)
        {
            _damageAmount = damage;
        }

        private void Start()
        {
            GetComponent<Rigidbody>().linearVelocity = transform.forward * _speed;
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasHit || other.CompareTag("Player"))
            {
                return;
            }

            var hitDamageable = false;

            if (other.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(_damageAmount);
                hitDamageable = true;
            }

            if (!other.isTrigger || hitDamageable)
            {
                _hasHit = true;
                Destroy(gameObject);
            }
        }
    }
}