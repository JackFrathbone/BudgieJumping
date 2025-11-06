using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GambleButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] BudgieType _budgieType;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _budgieNameAndCount;
    [SerializeField] Image _labelImage;
    [SerializeField] Image _gridImage;

    [SerializeField] TextMeshProUGUI _currentBudgieCountLabel;

    [Header("Data")]
    private int _currentBudgieCount;

    private void OnEnable()
    {
        UpdateText();

        Time.timeScale = 1f;
    }

    private void UpdateText()
    {
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

        _currentBudgieCountLabel.text = _currentBudgieCount.ToString();
    }

    public void AddBudgie()
    {
        if (!CheckBudgieAvailable())
            return;

        RemoveBudgieFromPlayer();

        _gridImage.color = Color.red;
        _labelImage.color = Color.green;
        Invoke("ResetButton", 0.5f);

        _currentBudgieCount++;

        UpdateText();
    }

    public void RemoveBudgie()
    {
        if (_currentBudgieCount <= 0)
            return;

        _gridImage.color = Color.green;
        _labelImage.color = Color.red;
        Invoke("ResetButton", 0.5f);

        _currentBudgieCount--;
        AddBudgiesToPlayer(1);

        UpdateText();
    }

    public void RemoveAllBudgies()
    {
        _currentBudgieCount = 0;

        _gridImage.color = Color.red;
        Invoke("ResetButton", 0.5f);

        UpdateText();
    }

    private bool CheckBudgieAvailable()
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                if (GameController.GreenBudgieCount >= 1)
                {
                    return true;
                }
                break;
            case BudgieType.Blue:
                if (GameController.BlueBudgieCount >= 1)
                {
                    return true;
                }
                break;
            case BudgieType.Red:
                if (GameController.RedBudgieCount >= 1)
                {
                    return true;
                }
                break;
            case BudgieType.Gold:
                if (GameController.GoldBudgieCount >= 1)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    private void AddBudgiesToPlayer(int i)
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                GameController.GreenBudgieCount += i;
                break;
            case BudgieType.Blue:
                GameController.BlueBudgieCount += i;
                break;
            case BudgieType.Red:
                GameController.RedBudgieCount += i;
                break;
            case BudgieType.Gold:
                GameController.GoldBudgieCount += i;
                break;
        }
    }

    private void RemoveBudgieFromPlayer()
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                GameController.GreenBudgieCount -= 1;
                break;
            case BudgieType.Blue:
                GameController.BlueBudgieCount -= 1;
                break;
            case BudgieType.Red:
                GameController.RedBudgieCount -= 1;
                break;
            case BudgieType.Gold:
                GameController.GoldBudgieCount -= 1;
                break;
        }
    }

    private void ResetButton()
    {
        _gridImage.color = Color.white;
        _labelImage.color = Color.white;
    }

    private bool FiftyFifty()
    {
        if (Random.Range(0, 2) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
