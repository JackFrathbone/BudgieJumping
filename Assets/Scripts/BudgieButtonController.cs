using TMPro;
using UnityEngine;

public class BudgieButtonController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int _baseCost;
    [SerializeField] int _maxIncrease;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _budgieCost;

    [Header("Data")]
    private int _cost;

    private void OnEnable()
    {
        AdjustCost();
        UpdateLabel();
    }

    private void AdjustCost()
    {
        //Adjust the cost randomly
        _cost = Random.Range(_baseCost + -_maxIncrease, _baseCost + _maxIncrease);
    }

    private void UpdateLabel()
    {
        _budgieCost.text = "$" + _cost.ToString();
    }
}
