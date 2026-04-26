using Assets._Project.Scripts.Core;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        [Header("Melee Settings")][SerializeField] private float _attackRange = 2.5f;
        [SerializeField] private float _attackRadius = 0.5f;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _hitLayers;

        [Header("Visuals & Animation")]
        [SerializeField] private Transform _weaponModel;
        [SerializeField] private float _swingDuration = 0.25f; 

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private bool _isSwinging;

        public override bool CanAttack()
        {
            return base.CanAttack() && !_isSwinging;
        }

        public override void Attack()
        {
            _lastAttackTime = Time.time;

            if (_weaponModel != null)
            {
                StartCoroutine(SwingAnimation());
            }

            if (Physics.SphereCast(_attackPoint.position, _attackRadius, _attackPoint.forward, out RaycastHit hit, _attackRange, _hitLayers)
                && hit.collider.TryGetComponent(out IDamageable damageableTarget))
            {
                damageableTarget.TakeDamage(_damage);
                Debug.Log($"Melee hit: {hit.collider.name} for {_damage} damage!");
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

        private IEnumerator SwingAnimation()
        {
            _isSwinging = true;

            var time = 0f;
            var half = _swingDuration * 0.5f;

            var startRot = _initialRotation * Quaternion.Euler(-20f, 40f, 20f);  
            var endRot = _initialRotation * Quaternion.Euler(10f, -10f, -10f);

            var startPos = _initialPosition + new Vector3(0.15f, 0.1f, 0.2f);
            var endPos = _initialPosition + new Vector3(-0.05f, -0.15f, 0.4f);

            while (time < half)
            {
                var t = time / half;
                t = t * t * (3f - 2f * t);

                _weaponModel.SetLocalPositionAndRotation(
                    Vector3.Lerp(_initialPosition, startPos, t),
                    Quaternion.Slerp(_initialRotation, startRot, t)
                );

                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;
            while (time < half)
            {
                var t = time / half;
                t = t * t * (3f - 2f * t);

                _weaponModel.SetLocalPositionAndRotation(
                    Vector3.Lerp(startPos, endPos, t),
                    Quaternion.Slerp(startRot, endRot, t)
                );

                time += Time.deltaTime;
                yield return null;
            }

            _weaponModel.SetLocalPositionAndRotation(_initialPosition, _initialRotation);

            _isSwinging = false;
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