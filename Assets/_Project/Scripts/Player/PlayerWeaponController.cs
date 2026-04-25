using Assets._Project.Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference _fireAction;

        [Header("State")]
        [SerializeField] private WeaponBase _currentWeapon;

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

        public void EquipWeapon(WeaponBase newWeapon)
        {
            _currentWeapon = newWeapon;
        }
    }
}