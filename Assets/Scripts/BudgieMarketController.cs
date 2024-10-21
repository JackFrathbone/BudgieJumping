using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BudgieMarketController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _playerMoneyLabel;

    [SerializeField] List<BudgieButtonController> budgieButtonControllers = new();

    private void OnEnable()
    {
        GameController.EnableCursor();

        UpdatePlayerMoneyLabel();
    }

    public void AdjustCosts()
    {
        foreach (BudgieButtonController button in budgieButtonControllers)
        {
            button.AdjustCost();
        }
    }

    public void UpdatePlayerMoneyLabel()
    {
        _playerMoneyLabel.text = "Money $" + GameController.PlayerMoney.ToString();
    }
}
