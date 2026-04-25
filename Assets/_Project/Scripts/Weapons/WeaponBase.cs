using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
	public abstract class WeaponBase : MonoBehaviour
	{
        [Header("Weapon Base Settings")]
        [SerializeField] protected float _damage = 20f;
        [SerializeField] protected float _attackCooldown = 0.5f;

        protected float _lastAttackTime;

        public virtual bool CanAttack()
        {
            return Time.time >= _lastAttackTime + _attackCooldown;
        }

        public abstract void Attack();
    }
}