using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Project.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

        [Header("Input")]
        [SerializeField] private InputActionReference _moveAction;

        [Header("References")]
        [SerializeField] private Animator _cameraAnimator;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed = 5f; 

        private CharacterController _characterController;
        private Vector3 _movementVector;
        private bool _isWalking;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable() => _moveAction.action.Enable();
        private void OnDisable() => _moveAction.action.Disable();

        private void Update()
        {
            CalculateHorizontalMovement();
            ApplyGravity();
            ApplyMovement();
            UpdateHeadbobAnimation();
        }

        private void CalculateHorizontalMovement()
        {
            var input = _moveAction.action.ReadValue<Vector2>();
            var inputVector = new Vector3(input.x, 0, input.y);

            _movementVector = transform.TransformDirection(inputVector) * _movementSpeed;
        }

        private void ApplyGravity()
        {
            _movementVector.y = -2f;
        }

        private void ApplyMovement()
        {
            _characterController.Move(_movementVector * Time.deltaTime);
        }

        private void UpdateHeadbobAnimation()
        {
            if (_cameraAnimator == null)
            {
                return;
            }

            var horizontalVelocity = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z);
            _isWalking = horizontalVelocity.magnitude > 0.1f;

            _cameraAnimator.SetBool(IsWalkingHash, _isWalking);
        }
    }
}