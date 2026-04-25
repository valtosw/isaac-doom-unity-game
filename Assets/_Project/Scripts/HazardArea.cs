using UnityEngine;

namespace Assets._Project.Scripts
{
    public class HazardArea : MonoBehaviour
    {
        [Header("Hazard Settings")]
        [SerializeField] private float damagePerTick = 10f;
        [SerializeField] private float tickRate = 1f;

        private float _nextDamageTime;

        private void OnTriggerStay(Collider other)
        {
            if (Time.time < _nextDamageTime)
            {
                return;
            }

            if (other.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(damagePerTick);
                _nextDamageTime = Time.time + tickRate;
            }
        }
    }
}