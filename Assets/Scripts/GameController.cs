using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static bool Lost;
    public static bool InMenu;

    [Header("Screens")]
    [SerializeField] GameObject _market;
    [SerializeField] GameObject _payment;
    [SerializeField] GameObject _debt;
    [SerializeField] GameObject _win;
    [SerializeField] GameObject _lose;

    [Header("References")]
    [SerializeField] BudgieMarketController _budgieMarketController;
    [SerializeField] CostController _costController;

    public static Color GoodGreen = new Color32(5, 159, 0, 255);

    private void Start()
    {
        PlayerMoney = 0;
        PlayerDebt = 1000;

        GreenBudgieCount = 0;
        BlueBudgieCount = 0;
        RedBudgieCount = 0;
        GoldBudgieCount = 0;

        Paused = false;
        Lost = false;
        InMenu = false;
    }

    public void WinGame()
    {
        EnableCursor();
        _win.gameObject.SetActive(true);
    }

    public void LoseGame()
    {
        EnableCursor();
        _lose.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Paused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMarketScreen()
    {
        EnableCursor();
        Paused = true;

        _budgieMarketController.AdjustCosts();
        _market.SetActive(true);

        _costController.AdjustCost();
    }

    public void ClosePaymentScreen()
    {
        if (Lost)
        {
            _debt.SetActive(true);
            _market.SetActive(false);
            _payment.SetActive(false);
            LoseGame();
            return;
        }

        _debt.SetActive(true);
        _market.SetActive(false);
        _payment.SetActive(false);
    }

    public void CloseDebtScreen()
    {
        if (PlayerDebt > 0)
        {
            _debt.SetActive(false);

            DisableCursor();
            Paused = false;
            InMenu = false;
        }
        else
        {
            Paused = true;
            EnableCursor();
            _debt.SetActive(false);
            WinGame();
        }

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

    private void OnApplicationQuit()
    {
        Application.Quit();
    }

    public static void Pause()
    {
        Time.timeScale = 0f;

        Paused = true;
        EnableCursor();
    }

    public static void Unpause()
    {
        if (InMenu)
            return;

        Time.timeScale = 1f;

        Paused = false;
        DisableCursor();
    }

    public static void LoadStart()
    {
        Time.timeScale = 1f;

        InMenu = false;
        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(0);
    }

    public static void LoadIntro()
    {
        Time.timeScale = 1f;

        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(1);
    }

    public static void LoadGame()
    {
        Time.timeScale = 1f;

        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(2);
    }
}
