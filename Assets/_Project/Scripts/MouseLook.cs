using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 0.2f;
    [SerializeField] private float smoothing = 20f;

    private float _xMousePosition;
    private float _smoothedMousePosition;
    private float _currentLookingPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    private void GetInput()
    {
        _xMousePosition = lookAction.action.ReadValue<Vector2>().x;
    }

    private void ModifyInput()
    {
        var targetMouseX = _xMousePosition * mouseSensitivity;
        _smoothedMousePosition = Mathf.Lerp(_smoothedMousePosition, targetMouseX, smoothing * Time.deltaTime);
    }

    private void MovePlayer()
    {
        _currentLookingPosition += _smoothedMousePosition;
        transform.localRotation = Quaternion.AngleAxis(_currentLookingPosition, Vector3.up);
    }
}