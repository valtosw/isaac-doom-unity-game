using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    public class RangedWeapon : WeaponBase
    {
        [Header("Ranged Settings")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _firePoint;

        public override void Attack()
        {
            _lastAttackTime = Time.time;

            var spawnedProjectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

            if (spawnedProjectile.TryGetComponent(out ProjectileBehavior projectile))
            {
                projectile.Initialize(_damage);
            }
        }
    }
}