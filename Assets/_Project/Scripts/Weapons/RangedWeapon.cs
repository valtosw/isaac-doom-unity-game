using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    public class RangedWeapon : WeaponBase
    {
        [Header("Ranged Settings")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _firePoint;

        [Header("Visuals & Animation")]
        [SerializeField] private Transform _weaponModel;
        [SerializeField] private float _castDuration = 0.2f;
        [SerializeField] private Vector3 _castOffset = new(0f, 0f, 0.3f);
        [SerializeField] private Vector3 _castRotation = new(-20f, 0f, 0f);

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private bool _isCasting;

        public override bool CanAttack()
        {
            return base.CanAttack() && !_isCasting;
        }

        public override void Attack()
        {
            _lastAttackTime = Time.time;

            if (_weaponModel != null)
            {
                StartCoroutine(CastAnimation());
            }

            var spawnedProjectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

            if (spawnedProjectile.TryGetComponent(out ProjectileBehavior projectile))
            {
                projectile.Initialize(_damage);
            }
        }

        private void Awake()
        {
            if (_weaponModel != null)
            {
                _initialPosition = _weaponModel.localPosition;
                _initialRotation = _weaponModel.localRotation;
            }
        }

        private IEnumerator CastAnimation()
        {
            _isCasting = true;
            var time = 0f;
            var targetPosition = _initialPosition + _castOffset;
            var targetRotation = _initialRotation * Quaternion.Euler(_castRotation);

            var halfDuration = _castDuration / 2f;

            while (time < halfDuration)
            {
                var t = time / halfDuration;
                t = t * t * (3f - 2f * t); 

                _weaponModel.SetLocalPositionAndRotation(Vector3.Lerp(_initialPosition, targetPosition, t), Quaternion.Slerp(_initialRotation, targetRotation, t));
                time += Time.deltaTime;
                yield return null;
            }

            time = 0;
            while (time < halfDuration)
            {
                var t = time / halfDuration;
                t = t * t * (3f - 2f * t);

                _weaponModel.SetLocalPositionAndRotation(Vector3.Lerp(targetPosition, _initialPosition, t), Quaternion.Slerp(targetRotation, _initialRotation, t));
                time += Time.deltaTime;
                yield return null;
            }

            _weaponModel.SetLocalPositionAndRotation(_initialPosition, _initialRotation);
            _isCasting = false;
        }
    }
}