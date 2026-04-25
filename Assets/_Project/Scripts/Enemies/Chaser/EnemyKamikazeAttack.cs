using Assets._Project.Scripts.Core;
using UnityEngine;

namespace Assets._Project.Scripts.Enemies.Chaser
{
    [RequireComponent(typeof(HealthComponent))]
    public class EnemyKamikazeAttack : MonoBehaviour
	{
        [Header("Settings")]
        [SerializeField] private float _damageAmount = 20f;

        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !other.TryGetComponent(out IDamageable damageableTarget))
            {
                return;
            }

            damageableTarget.TakeDamage(_damageAmount);
            TriggerExplosion();
        }

        private void TriggerExplosion()
        {
            if (_healthComponent != null)
            {
                _healthComponent.TakeDamage(_healthComponent.MaxHealth);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}