using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts.Player
{
    public class PlayerCameraLook : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference _lookAction;

        [Header("Settings")]
        [SerializeField] private float _mouseSensitivity = 0.1f;
        [SerializeField] private float _smoothing = 30f;

        private float _xMouseInput;
        private float _smoothedMouseInput;
        private float _currentLookingRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable() => _lookAction.action.Enable();
        private void OnDisable() => _lookAction.action.Disable();

        private void Update()
        {
            ReadInput();
            SmoothInput();
            ApplyRotation();
        }

        private void ReadInput()
        {
            _xMouseInput = _lookAction.action.ReadValue<Vector2>().x;
        }

        private void SmoothInput()
        {
            var targetRotationX = _xMouseInput * _mouseSensitivity;
            _smoothedMouseInput = Mathf.Lerp(_smoothedMouseInput, targetRotationX, _smoothing * Time.deltaTime);
        }

        private void ApplyRotation()
        {
            _currentLookingRotation += _smoothedMouseInput;
            transform.localRotation = Quaternion.AngleAxis(_currentLookingRotation, Vector3.up);
        }
    }
}