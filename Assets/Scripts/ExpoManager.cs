using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpoManager : MonoBehaviour
{
    [SerializeField, Tooltip("How long to wait with no input before resetting")] private float expoResetTime = 30f;
    private float _inputTimer;

    public static ExpoManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        _inputTimer += Time.deltaTime;

        //Check for mouse movement
        float mouseXDelta = Input.GetAxis("Mouse X");
        float mouseYDelta = Input.GetAxis("Mouse Y");
        if (mouseXDelta != 0 || mouseYDelta != 0)
        {
            _inputTimer = 0;
        }

        //Check for other input
        if (Input.anyKey)
        {
            _inputTimer = 0;
        }

        if (_inputTimer >= expoResetTime)
        {
            _inputTimer = 0;
            GameController.LoadStart();
        }
    }
}
