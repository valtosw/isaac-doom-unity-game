using Assets._Project.Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference _fireAction;

        [Header("Weapons Setup")]
        [SerializeField] private WeaponBase[] _allWeapons;

        [Header("State")]
        [SerializeField] private WeaponBase _currentWeapon;

        public void EquipWeapon(WeaponBase newWeapon)
        {
            _currentWeapon = newWeapon;

            foreach (var weapon in _allWeapons)
            {
                if (weapon != null)
                {
                    weapon.gameObject.SetActive(weapon == _currentWeapon);
                }
            }
        }

        private void Start()
        {
            if (_allWeapons == null || _allWeapons.Length == 0)
            {
                _allWeapons = GetComponentsInChildren<WeaponBase>(true);
            }

            if (_currentWeapon != null)
            {
                EquipWeapon(_currentWeapon);
            }
            else if (_allWeapons.Length > 0)
            {
                EquipWeapon(_allWeapons[0]);
            }
        }


        private void OnEnable()
        {
            _fireAction.action.Enable();
            _fireAction.action.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            _fireAction.action.performed -= OnFirePerformed;
            _fireAction.action.Disable();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            if (_currentWeapon != null && _currentWeapon.CanAttack())
            {
                _currentWeapon.Attack();
            }
        }
    }
}