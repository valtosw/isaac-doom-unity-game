using UnityEngine;

namespace Assets._Project.Scripts
{
    public class MeleeWeapon : Weapon
    {
        private static readonly int MeleeSwingHash = Animator.StringToHash("MeleeSwing");

        [Header("Settings")]
        [SerializeField] private float attackRange = 2.5f;
        [SerializeField] private float attackRadius = 0.5f;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask hitLayers;
        [SerializeField] private Animator weaponAnimator;

        public override void Attack()
        {
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger(MeleeSwingHash);
            }

            _lastAttackTime = Time.time;

            if (Physics.SphereCast(attackPoint.position, attackRadius, attackPoint.forward, out RaycastHit hit, attackRange, hitLayers)
                && hit.collider.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(Damage);
                Debug.Log($"Melee hit: {hit.collider.name} for {Damage} damage!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position + attackPoint.forward * attackRange, attackRadius);
        }
    }
}