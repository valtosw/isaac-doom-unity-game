using UnityEngine;

namespace Assets._Project.Scripts
{
    public class RangedWeapon : Weapon
    {
        [Header("Ranged Settings")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

        public override void Attack()
        {
            _lastAttackTime = Time.time;

            var spawnedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            if (spawnedProjectile.TryGetComponent(out Projectile projectileComponent))
            {
                projectileComponent.Initialize(Damage);
            }
        }
    }
}