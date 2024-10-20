using TMPro;
using UnityEngine;

public class BudgieButtonController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int _baseCost;
    [SerializeField] int _maxIncrease;

    [SerializeField] BudgieType _budgieType;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _budgieNameAndCount;
    [SerializeField] TextMeshProUGUI _budgieCost;
    [SerializeField] BudgieMarketController _budgieMarketController;

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
        _budgieMarketController.UpdatePlayerMoneyLabel();

        _budgieCost.text = "$" + _cost.ToString();

        switch (_budgieType)
        {
            case BudgieType.Green:
                _budgieNameAndCount.text = $"Green Budgie ({GameController.GreenBudgieCount})";
                break;
            case BudgieType.Blue:
                _budgieNameAndCount.text = $"Blue Budgie ({GameController.BlueBudgieCount})";
                break;
            case BudgieType.Red:
                _budgieNameAndCount.text = $"Red Budgie ({GameController.RedBudgieCount})";
                break;
            case BudgieType.Gold:
                _budgieNameAndCount.text = $"Gold Budgie ({GameController.GoldBudgieCount})";
                break;
        }
    }

    public void SellBudgie()
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                if (GameController.GreenBudgieCount > 0)
                {
                    GameController.AddBudgieCount(BudgieType.Green, -1);
                    GameController.PlayerMoney += _cost;
                }
                break;
            case BudgieType.Blue:
                if (GameController.BlueBudgieCount > 0)
                {
                    GameController.AddBudgieCount(BudgieType.Blue, -1);
                    GameController.PlayerMoney += _cost;
                }
                break;
            case BudgieType.Red:
                if (GameController.RedBudgieCount > 0)
                {
                    GameController.AddBudgieCount(BudgieType.Red, -1);
                    GameController.PlayerMoney += _cost;
                }
                break;
            case BudgieType.Gold:
                if (GameController.GoldBudgieCount > 0)
                {
                    GameController.AddBudgieCount(BudgieType.Gold, -1);
                    GameController.PlayerMoney += _cost;
                }
                break;
        }

        UpdateLabel();
    }

    public void BuyBudgie()
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                if (GameController.PlayerMoney >= _cost)
                {
                    GameController.AddBudgieCount(BudgieType.Green, 1);
                    GameController.PlayerMoney -= _cost;
                }
                break;
            case BudgieType.Blue:
                if (GameController.PlayerMoney >= _cost)
                {
                    GameController.AddBudgieCount(BudgieType.Blue, 1);
                    GameController.PlayerMoney -= _cost;
                }
                break;
            case BudgieType.Red:
                if (GameController.PlayerMoney >= _cost)
                {
                    GameController.AddBudgieCount(BudgieType.Red, 1);
                    GameController.PlayerMoney -= _cost;
                }
                break;
            case BudgieType.Gold:
                if (GameController.PlayerMoney >= _cost)
                {
                    GameController.AddBudgieCount(BudgieType.Gold, 1);
                    GameController.PlayerMoney -= _cost;
                }
                break;
        }

        UpdateLabel();
    }
}
