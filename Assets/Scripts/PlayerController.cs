using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The height that the bungee will bounce back to the top from")]
    [SerializeField, Range(0, -100)] float _bungeeHeight;

    [Header("References")]
    [SerializeField] private Transform _bungeeOrigin;

    [Header("Data")]
    [SerializeField] float _speed = 5f;
    [SerializeField] float mouseSensitivity = 100f;

    [SerializeField] float _maxSpeed;
    [SerializeField] float _damping;

    [SerializeField] float rayLength = 100f;

    private Vector3 _targetPosition;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector3 _velocity = new();

    private bool _isJumping;
    private bool _isGoingUp;

    void Update()
    {
        UpdatePlayerCamera();
        CheckHeight();

        if (Input.GetMouseButtonDown(0) && !_isJumping && !_isGoingUp)
        {
            // Create a ray from the camera going forward
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                _targetPosition = hit.point;
                _isJumping = true;
            }
        }

        if (!_isGoingUp && _isJumping)
        {
            MoveToPosition(_targetPosition);
        }
        else if (_isGoingUp && !_isJumping)
        {
            MoveToPosition(_bungeeOrigin.position);
        }

    }

    private void CheckHeight()
    {
        if (transform.position.y <= _bungeeHeight)
        {
            _isJumping = false;
            _isGoingUp = true;
        }

        if (transform.position.y > -0.5f)
        {
            _isGoingUp = false;
        }
    }

    private void UpdatePlayerCamera()
    {
        // Mouse movement
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the x and y rotations based on mouse input
        xRotation -= mouseY;
        yRotation += mouseX;

        // Clamp the up/down rotation to avoid flipping (optional, but usually desirable for a first-person camera)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera (rotate in 360 degrees)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void MoveToPosition(Vector3 target)
    {
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        var n1 = _velocity - _damping * _damping * Time.deltaTime * (transform.position - target);
        var n2 = 1 + _damping * Time.deltaTime;
        _velocity = n1 / (n2 * n2);

        transform.position += _velocity * Time.deltaTime;
    }
}
