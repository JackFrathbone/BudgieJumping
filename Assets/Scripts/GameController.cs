using UnityEngine;

public enum BudgieType
{
    Green,
    Blue,
    Red,
    Gold
}

public class GameController : MonoBehaviour
{
    [Header("Player Stats")]
    public static int PlayerMoney = 0;
    public static int PlayerDebt = 1000;

    public static int GreenBudgieCount = 0;
    public static int BlueBudgieCount = 0;
    public static int RedBudgieCount = 0;
    public static int GoldBudgieCount = 0;

    [Header("States")]
    public static bool Paused;

    [Header("Screens")]
    [SerializeField] GameObject _market;
    [SerializeField] GameObject _payment;

    private void Start()
    {
        DisableCursor();
    }

    public void OpenMarketScreen()
    {
        EnableCursor();
        Paused = true;
        _market.SetActive(true);
    }

    public void ClosePaymentScreen()
    {
        DisableCursor();
        Paused = false;
        _payment.SetActive(false);
    }

    public static void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public static void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void AddBudgieCount(BudgieType type, int i)
    {
        switch (type)
        {
            case BudgieType.Green:
                GreenBudgieCount += i;
                break;
            case BudgieType.Blue:
                BlueBudgieCount += i;
                break;
            case BudgieType.Red:
                RedBudgieCount += i;
                break;
            case BudgieType.Gold:
                GoldBudgieCount += i;
                break;
        }
    }
}
