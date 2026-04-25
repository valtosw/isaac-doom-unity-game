using Assets._Project.Scripts.Core;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        private static readonly int MeleeSwingHash = Animator.StringToHash("MeleeSwing");

        [Header("Melee Settings")]
        [SerializeField] private float _attackRange = 2.5f;
        [SerializeField] private float _attackRadius = 0.5f;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _hitLayers;

        [Header("References")]
        [SerializeField] private Animator _weaponAnimator;

        public override void Attack()
        {
            _lastAttackTime = Time.time;

            if (_weaponAnimator != null)
            {
                _weaponAnimator.SetTrigger(MeleeSwingHash);
            }

            if (Physics.SphereCast(_attackPoint.position, _attackRadius, _attackPoint.forward, out RaycastHit hit, _attackRange, _hitLayers)
                && hit.collider.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(_damage);
                Debug.Log($"Melee hit: {hit.collider.name} for {_damage} damage!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPoint.position + _attackPoint.forward * _attackRange, _attackRadius);
        }
    }
}