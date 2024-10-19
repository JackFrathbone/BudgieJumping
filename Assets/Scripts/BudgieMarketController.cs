using TMPro;
using UnityEngine;

public class BudgieMarketController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _playerMoneyLabel;

    private void OnEnable()
    {
        GameController.EnableCursor();

        UpdatePlayerMoneyLabel(GameController.playerMoney);
    }

    private void UpdatePlayerMoneyLabel(int i)
    {
        _playerMoneyLabel.text = "Money $" + i.ToString();
    }
}
