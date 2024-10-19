using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Player Stats")]
    public static int PlayerMoney = 0;
    public static int PlayerDebt = 1000;

    public static int GreenBudgie;
    public static int BlueBudgie;
    public static int RedBudgie;
    public static int GoldBudie;

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
}
