using UnityEngine;

namespace Assets._Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 5f;

        private float _damageAmount;

        public void Initialize(float damage)
        {
            _damageAmount = damage;
        }

        private void Start()
        {
            GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            if (other.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(_damageAmount);
            }

            if (!other.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}