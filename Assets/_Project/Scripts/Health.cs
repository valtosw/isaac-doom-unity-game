using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project.Scripts
{
	public class Health: MonoBehaviour, IDamageable
	{
        [Header("Settings")]
        [SerializeField] private float maxHealth = 100f;

        private float _currentHealth;
        private bool _isDead;

        [Header("Events")]
        public UnityEvent<float, float> OnHealthChanged;
        public UnityEvent OnDeath;
        public UnityEvent OnDamageTaken;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }

        public void TakeDamage(float damageAmount)
        {
            if (_isDead || damageAmount <= 0)
            {
                return;
            }

            _currentHealth -= damageAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnDamageTaken?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);

            Debug.Log($"{gameObject.name} took {damageAmount} damage. HP: {_currentHealth}/{maxHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float healAmount)
        {
            if (_isDead || healAmount <= 0)
            {
                return;
            }

            _currentHealth += healAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }

        private void Die()
        {
            _isDead = true;
            Debug.Log($"{gameObject.name} has died!");

            OnDeath?.Invoke();
        }
    }
}