using UnityEngine;

namespace Assets._Project.Scripts
{
    [RequireComponent(typeof(Health))]
    public class KamikazeAttack: MonoBehaviour
	{
        [Header("Settings")]
        [SerializeField] private float damageAmount = 20f;

        private Health _myHealth;

        private void Awake()
        {
            _myHealth = GetComponent<Health>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !other.TryGetComponent(out IDamageable damageableTarget))
            {
                return;
            }

            damageableTarget.TakeDamage(damageAmount);
            Explode();
        }

        private void Explode()
        {
            if (_myHealth != null)
            {
                _myHealth.TakeDamage(_myHealth.MaxHealth);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}