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

    [Header("Screens")]
    [SerializeField] GameObject _market;
    [SerializeField] GameObject _payment;
    [SerializeField] GameObject _debt;
    [SerializeField] GameObject _win;
    [SerializeField] GameObject _lose;

    [Header("References")]
    [SerializeField] BudgieMarketController _budgieMarketController;
    [SerializeField] CostController _costController;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            DisableCursor();
        }
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
        }
        else
        {
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

    public static void LoadStart()
    {
        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(0);
    }

    public static void LoadIntro()
    {
        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(1);
    }

    public static void LoadGame()
    {
        Paused = false;
        EnableCursor();
        SceneManager.LoadScene(2);
    }
}
