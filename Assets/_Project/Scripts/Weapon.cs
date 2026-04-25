using UnityEngine;

namespace Assets._Project.Scripts
{
	public abstract class Weapon: MonoBehaviour
	{
        protected float _lastAttackTime;

        [Header("Settings")]
        public float Damage = 20f;
        public float AttackCooldown = 0.5f;

        public virtual bool CanAttack()
        {
            return Time.time >= _lastAttackTime + AttackCooldown;
        }

        public abstract void Attack();
    }
}