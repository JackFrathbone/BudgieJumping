using TMPro;
using UnityEngine;

public class BudgieMarketController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _playerMoneyLabel;

    private void OnEnable()
    {
        GameController.EnableCursor();

        UpdatePlayerMoneyLabel();
    }

    public void UpdatePlayerMoneyLabel()
    {
        _playerMoneyLabel.text = "Money $" + GameController.PlayerMoney.ToString();
    }
}
