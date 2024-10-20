using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgieHolder : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _changeInterval = 0.5f;

    [SerializeField] float _minEscapeTime;
    [SerializeField] float _maxEscapeTime;

    [Header("References")]
    [SerializeField] PlayerController _controller;
    [SerializeField] Animator _animator;

    [Header("Data")]
    public bool HasBudgie;
    public BudgieController BudgieRef;

    private int _currentFrame = 0;
    private float _animTimer;
    private Image _image;

    private List<Sprite> _currentAnimation = new();

    private float _escapeTimer;
    private float _totalEscapeTime;

    [Header("Animations")]
    [SerializeField] List<Sprite> GreenBudgieAngryFrames = new();
    [SerializeField] List<Sprite> BlueBudgieAngryFrames = new();
    [SerializeField] List<Sprite> RedBudgieAngryFrames = new();
    [SerializeField] List<Sprite> GoldBudgieAngryFrames = new();

    [SerializeField] List<Sprite> GreenBudgieSadFrames = new();
    [SerializeField] List<Sprite> BlueBudgieSadFrames = new();
    [SerializeField] List<Sprite> RedBudgieSadFrames = new();
    [SerializeField] List<Sprite> GoldBudgieSadFrames = new();

    private void Start()
    {
        _controller = GameObject.FindObjectOfType<PlayerController>();

        _animator = GetComponent<Animator>();

        _image = GetComponent<Image>();
        _image.color = Color.clear;
    }

    private void Update()
    {
        if (_image == null || !HasBudgie)
        {
            return;
        }

        _escapeTimer -= Time.deltaTime;
        _animTimer += Time.deltaTime;

        if (_animTimer >= _changeInterval && _currentAnimation.Count > 0)
        {
            _currentFrame = (_currentFrame + 1) % _currentAnimation.Count;
            _image.sprite = _currentAnimation[_currentFrame];

            _animTimer = 0f;
        }

        if (_escapeTimer <= 0)
        {
            RemoveBudgie();
        }

        _animator.SetFloat("Speed", 1f - (_escapeTimer / _totalEscapeTime));
    }

    public void AddBudgie(BudgieType type, BudgieController budgie)
    {
        BudgieRef = budgie;

        HasBudgie = true;

        bool isSad = false;

        if (Random.Range(0, 2) == 1)
        {
            isSad = true;
        }

        if (isSad)
        {
            switch (type)
            {
                case BudgieType.Green:
                    _currentAnimation = GreenBudgieSadFrames;
                    break;
                case BudgieType.Blue:
                    _currentAnimation = BlueBudgieSadFrames;
                    break;
                case BudgieType.Red:
                    _currentAnimation = RedBudgieSadFrames;
                    break;
                case BudgieType.Gold:
                    _currentAnimation = GoldBudgieSadFrames;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case BudgieType.Green:
                    _currentAnimation = GreenBudgieAngryFrames;
                    break;
                case BudgieType.Blue:
                    _currentAnimation = BlueBudgieAngryFrames;
                    break;
                case BudgieType.Red:
                    _currentAnimation = RedBudgieAngryFrames;
                    break;
                case BudgieType.Gold:
                    _currentAnimation = GoldBudgieAngryFrames;
                    break;
            }
        }

        _image.color = Color.white;
        _image.sprite = _currentAnimation[0];

        _escapeTimer = Random.Range(_minEscapeTime, _maxEscapeTime);
        _totalEscapeTime = _escapeTimer;
    }

    public void RemoveBudgie()
    {
        _image.color = Color.clear;
        _image.sprite = null;

        HasBudgie = false;

        BudgieRef.EnableCaptureImmunity();
        _controller.DropBudgie(BudgieRef);

    }
}
