using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BudgieController : MonoBehaviour
{
    [Header("Settings")]
    public BudgieType BudgieType;
    [SerializeField] float _changeInterval = 0.5f;
    [SerializeField] float _flySpeed = 2.0f;

    [Header("References")]
    [SerializeField] Sprite[] _frames;
    private BirdManager _birdManager;

    [Header("Data")]
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;

    private int _currentFrame = 0;
    private float _animTimer;

    public Vector3 _flyTarget;

    private float _flyTimer;
    private float _flyTimerTarget;

    void Start()
    {
        _mainCamera = Camera.main;

        _birdManager = GameObject.FindObjectOfType<BirdManager>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _frames[_currentFrame];

        GetNewTargetPosition();
        _flyTimerTarget = Random.Range(10f, 15f);
    }

    void Update()
    {
        MoveToPosition();

        _animTimer += Time.deltaTime;
        _flyTimer += Time.deltaTime;

        if (_animTimer >= _changeInterval)
        {
            _currentFrame = (_currentFrame + 1) % _frames.Length;
            _spriteRenderer.sprite = _frames[_currentFrame];

            _animTimer = 0f;
        }

        if(_flyTimer >= _flyTimerTarget)
        {
            GetNewTargetPosition();

            _flyTimerTarget = Random.Range(3f, 10f);
            _flyTimer = 0f;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(_mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }

    private void MoveToPosition()
    {
        var step = _flySpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _flyTarget, step);
    }

    private void GetNewTargetPosition()
    {
        _flyTarget = _birdManager.GetRandomBirdSpawnerPosition();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_flyTarget, 1);
    }
}
