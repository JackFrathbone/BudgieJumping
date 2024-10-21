using System.Collections;
using TMPro;
using UnityEngine;

public class DebtController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _moneyLeftText;
    [SerializeField] TextMeshProUGUI _debtLeftText;

    [SerializeField] GameObject _continueButton;

    [Header("Data")]
    private int _calculatedDebt;

    private void OnEnable()
    {
        _continueButton.SetActive(false);

        _moneyLeftText.text = "$" + GameController.PlayerMoney.ToString();
        _debtLeftText.text = "$" + GameController.PlayerDebt.ToString();

        if(GameController.PlayerMoney >= 0)
        {
            _moneyLeftText.color = GameController.GoodGreen;
        }
        else
        {
            _moneyLeftText.color = Color.red;
        }

        StopAllCoroutines();

        _calculatedDebt = GameController.PlayerDebt - GameController.PlayerMoney;

        StartCoroutine("DebtAnimation");
    }

    IEnumerator DebtAnimation()
    {
        int displayMoney = GameController.PlayerMoney;
        int displayDebt = GameController.PlayerDebt;
        if (displayMoney >= 0)
        {
            _debtLeftText.color = GameController.GoodGreen;

            while (displayDebt > _calculatedDebt)
            {
                displayMoney--;
                displayDebt--;

                _moneyLeftText.text = "$" + displayMoney.ToString();
                _debtLeftText.text = "$" + displayDebt.ToString();
                yield return new WaitForSeconds(0.035f);
            }
        }
        else
        {
            _debtLeftText.color = Color.red;

            while (displayDebt < _calculatedDebt)
            {
                displayMoney++;
                displayDebt++;

                _moneyLeftText.text = "$" + displayMoney.ToString();
                _debtLeftText.text = "$" + displayDebt.ToString();
                yield return new WaitForSeconds(0.035f);
            }
        }


        GameController.PlayerMoney = 0;
        _moneyLeftText.color = Color.black;

        GameController.PlayerDebt = _calculatedDebt;

        _continueButton.SetActive(true);
    }
}
