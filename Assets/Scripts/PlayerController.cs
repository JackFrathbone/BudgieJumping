using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _bungeeOrigin;

    [Header("Data")]
    private Vector3 _targetPosition;

    public float _speed = 5f;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    [SerializeField] float _height;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _damping;

    private Vector3 _velocity = new Vector3();

    public float rayLength = 100f;


    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdatePlayerCamera();

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera going forward
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                _targetPosition = hit.point;
                Debug.Log(_targetPosition);
            }
        }
        
        if(Input.GetMouseButton(0))
        {
            MoveToPosition(_targetPosition);
            
        }
        else
        {
            MoveToPosition(_bungeeOrigin.position);
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

        var n1 = _velocity - (transform.position - target) * _damping * _damping * Time.deltaTime;
        var n2 = 1 + _damping * Time.deltaTime;
        _velocity = n1 / (n2 * n2);

        transform.position += _velocity * Time.deltaTime;
    }
}
