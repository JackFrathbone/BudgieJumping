using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The height that the bungee will bounce back to the top from")]
    [SerializeField, Range(0, -100)] float _bungeeHeight;

    [Header("References")]
    [SerializeField] private Transform _bungeeOrigin;
    [SerializeField] GameObject _cashInHudText;

    [SerializeField] GameController _gameController;

    [SerializeField] GameObject _speedlines;
    [SerializeField] GameObject _vignette;

    [SerializeField] GameObject _handsEmpty;
    [SerializeField] GameObject _handsFull;

    [SerializeField] List<BudgieHolder> _budgieHolders = new();

    [SerializeField] TextMeshProUGUI _bungeeHealthText;

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

    private bool _canCashIn;

    private List<BudgieController> _holdingBudgies = new();

    private int _bungeeHealth = 100;

    private void Start()
    {
        _speedlines.SetActive(false);
        _vignette.SetActive(false);
    }

    void Update()
    {
        if (GameController.Paused)
        {
            transform.position = _bungeeOrigin.position;
            return;
        }

        UpdatePlayerCamera();
        CheckHeight();
        UpdateHands();

        //For cash in
        if (_canCashIn)
        {
            _cashInHudText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                _bungeeHealth = 100;
                CalculateBudgies();
                _gameController.OpenMarketScreen();
            }

        }
        else
        {
            _cashInHudText.SetActive(false);
        }


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

                //Take away bungee health
                _bungeeHealth -= Random.Range(2, 11);
            }
        }

        if (!_isGoingUp && _isJumping)
        {
            _speedlines.SetActive(true);
            _vignette.SetActive(true);
            MoveToPosition(_targetPosition);
        }
        else if (_isGoingUp && !_isJumping)
        {
            _speedlines.SetActive(false);
            _vignette.SetActive(true);
            MoveToPosition(_bungeeOrigin.position);
        }
        else if (!_isGoingUp && !_isJumping)
        {
            _speedlines.SetActive(false);
            _vignette.SetActive(false);
        }

        _bungeeHealthText.text = $"Rope Integrity - {_bungeeHealth}";

        if(_bungeeHealth <= 0)
        {
            _gameController.LoseGame();
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

    private void UpdateHands()
    {
        if(_holdingBudgies.Count > 0)
        {
            _handsEmpty.SetActive(false);
            _handsFull.SetActive(true);
        }
        else
        {
            _handsEmpty.SetActive(true);
            _handsFull.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Budgie"))
        {
            _canCashIn = true;
        }
        else
        {
            PickUpBudgie(other.GetComponent<BudgieController>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Budgie"))
        {
            _canCashIn = false;
        }
    }

    private void PickUpBudgie(BudgieController pickedUpBudgie)
    {
        pickedUpBudgie.gameObject.SetActive(false);
        _holdingBudgies.Add(pickedUpBudgie);

        foreach(BudgieHolder budgieHolder in _budgieHolders)
        {
            if (!budgieHolder.HasBudgie)
            {
                budgieHolder.AddBudgie(pickedUpBudgie.BudgieType, pickedUpBudgie);
                break;
            }
        }
    }

    private void CalculateBudgies()
    {
        foreach(BudgieController budgie in _holdingBudgies)
        {
            switch (budgie.BudgieType)
            {
                case BudgieType.Green:
                    GameController.AddBudgieCount(BudgieType.Green, 1);
                    break;
                case BudgieType.Blue:
                    GameController.AddBudgieCount(BudgieType.Blue, 1);
                    break;
                case BudgieType.Red:
                    GameController.AddBudgieCount(BudgieType.Red, 1);
                    break;
                case BudgieType.Gold:
                    GameController.AddBudgieCount(BudgieType.Gold, 1);
                    break;
            }
        }

        //
        foreach(BudgieHolder holder in _budgieHolders)
        {
            if (holder.HasBudgie)
            {
                holder.RemoveBudgie();
            }
        }

        //Clear scene stuff
        _holdingBudgies.Clear();

        List<BudgieController> sceneBudgies = new();
        sceneBudgies = FindObjectsOfType<BudgieController>().ToList<BudgieController>();

        foreach(BudgieController budgieController in sceneBudgies)
        {
            Destroy(budgieController.gameObject, 1f);
        }
    }

    public void DropBudgie(BudgieController budgie)
    {
        budgie.gameObject.SetActive(true);
        budgie.transform.position = transform.position;

        _holdingBudgies.Remove(budgie);
    }
}
