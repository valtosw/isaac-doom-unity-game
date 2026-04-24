using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;

    [Header("References")]
    [SerializeField] private Animator cameraAnimator;

    [Header("Settings")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController _characterController;
    private Vector3 _movementVector;
    private float _verticalVelocity;
    private bool _isWalking;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        CalculateMovement();
        ApplyGravity();
        MovePlayer();
        CheckForHeadbob();

        if (cameraAnimator != null)
        {
            cameraAnimator.SetBool(IsWalkingHash, _isWalking);
        }
    }

    private void CalculateMovement()
    {
        var input = moveAction.action.ReadValue<Vector2>();
        var inputVector = new Vector3(input.x, 0, input.y);

        _movementVector = transform.TransformDirection(inputVector) * playerSpeed;
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        _verticalVelocity += gravity * Time.deltaTime;
        _movementVector.y = _verticalVelocity;
    }

    private void MovePlayer()
    {
        _characterController.Move(_movementVector * Time.deltaTime);
    }

    private void CheckForHeadbob()
    {
        var horizontalVelocity = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z);
        _isWalking = horizontalVelocity.magnitude > 0.1f;
    }
}