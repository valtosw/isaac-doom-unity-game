using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference fireAction;

        [Header("Equipped Weapon")]
        [SerializeField] private Weapon currentWeapon;

        private void OnEnable()
        {
            fireAction.action.Enable();
            fireAction.action.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            fireAction.action.performed -= OnFirePerformed;
            fireAction.action.Disable();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            if (currentWeapon != null && currentWeapon.CanAttack())
            {
                currentWeapon.Attack();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
        }
    }
}