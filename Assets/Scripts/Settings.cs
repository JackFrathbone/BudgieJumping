using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] float _defaultSensitivity = 50f;
    [SerializeField] Slider _sensitivitySlider;

    public static float MouseSensitivity = 50;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
            MouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        else
        {
            PlayerPrefs.SetFloat("Sensitivity", _defaultSensitivity);

            MouseSensitivity = _defaultSensitivity;
        }

        _sensitivitySlider.maxValue = 100f;
        _sensitivitySlider.value = MouseSensitivity;
    }

    private void OnEnable()
    {
        _sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    private void OnDisable()
    {
        _sensitivitySlider.onValueChanged.RemoveAllListeners();
    }

    private void UpdateSensitivity(float amount)
    {
        PlayerPrefs.SetFloat("Sensitivity", amount);
        MouseSensitivity = amount;
    }
}
