using Assets._Project.Scripts.Core;
using UnityEngine;

namespace Assets._Project.Scripts.Environment
{
    public class DamageZone : MonoBehaviour
    {
        [Header("Hazard Settings")]
        [SerializeField] private float _damagePerTick = 10f;
        [SerializeField] private float _tickRate = 1f;

        private float _nextDamageTime;

        private void OnTriggerStay(Collider other)
        {
            if (Time.time < _nextDamageTime || !other.TryGetComponent(out IDamageable damageableTarget))
            {
                return;
            }

            damageableTarget.TakeDamage(_damagePerTick);
            _nextDamageTime = Time.time + _tickRate;
        }
    }
}