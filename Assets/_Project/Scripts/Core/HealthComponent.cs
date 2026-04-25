using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project.Scripts.Core
{
	public class HealthComponent : MonoBehaviour, IDamageable
	{
        [Header("Settings")]
        [SerializeField] private float _maxHealth = 100f;

        [Header("Events")]
        public UnityEvent<float, float> OnHealthChanged;
        public UnityEvent OnDeath;
        public UnityEvent OnDamageTaken;

        private float _currentHealth;
        private bool _isDead;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        public void TakeDamage(float damageAmount)
        {
            if (_isDead || damageAmount <= 0)
            {
                return;
            }

            _currentHealth = Mathf.Clamp(_currentHealth - damageAmount, 0, _maxHealth);

            OnDamageTaken?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            Debug.Log($"[{gameObject.name}] took {damageAmount} damage. HP: {_currentHealth}/{_maxHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private void Die()
        {
            _isDead = true;
            Debug.Log($"[{gameObject.name}] has died!");
            OnDeath?.Invoke();
        }
    }
}