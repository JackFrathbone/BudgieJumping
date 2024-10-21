using TMPro;
using UnityEngine;

public class CostController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _dailyCost1Text;
    [SerializeField] TextMeshProUGUI _dailyCost2Text;
    [SerializeField] TextMeshProUGUI _dailyCost3Text;
    [SerializeField] TextMeshProUGUI _budgetText;

    [SerializeField] TextMeshProUGUI _playerMoneyLabelBefore;
    [SerializeField] TextMeshProUGUI _playerMoneyLabelAfter;

    [Header("Data")]
    private int _dailyCost1;
    private int _dailyCost2;
    private int _dailyCost3;

    private int _calcuatedMoneyLeft;

    private void OnEnable()
    {
        UpdateLabel();
        UpdatePlayerMoney();
    }

    public void AdjustCost()
    {
        //Adjust the cost randomly
        _dailyCost1 = 15;
        _dailyCost2 = 50;
        _dailyCost3 = Random.Range(25, 85);
    }

    private void UpdateLabel()
    {
        _dailyCost1Text.text = "-$" + _dailyCost1.ToString();
        _dailyCost2Text.text = "-$" + _dailyCost2.ToString();
        _dailyCost3Text.text = "-$" + _dailyCost3.ToString();

        _budgetText.text = "-$" + GameController.PlayerDebt;
    }

    private void UpdatePlayerMoney()
    {
        _playerMoneyLabelBefore.text = "+$" + GameController.PlayerMoney;

        //Calculate money after debt
        int dailySpend = _dailyCost1 + _dailyCost2 + _dailyCost3;

        _calcuatedMoneyLeft = GameController.PlayerMoney;
        _calcuatedMoneyLeft -= dailySpend;

        _playerMoneyLabelAfter.text = "$" + (_calcuatedMoneyLeft);

        if (_calcuatedMoneyLeft >= 0)
        {
            _playerMoneyLabelAfter.color = Color.black;
        }
        else
        {
            _playerMoneyLabelAfter.color = Color.red;
        }
    }

    public void SubmitPlayerMoneyChanges()
    {
        //Calculate money after debt
        int dailySpend = _dailyCost1 + _dailyCost2 + _dailyCost3;
        GameController.PlayerMoney -= dailySpend;
    }
}
