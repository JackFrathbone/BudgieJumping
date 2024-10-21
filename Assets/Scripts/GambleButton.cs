using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GambleButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] BudgieType _budgieType;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _budgieNameAndCount;
    [SerializeField] Image _image;

    private void OnEnable()
    {
        UpdateBirdText();
    }

    private void UpdateBirdText()
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
    }

    public void TryGamble()
    {
        switch (_budgieType)
        {
            case BudgieType.Green:
                if (GameController.GreenBudgieCount >= 1)
                {
                    if (FiftyFifty())
                    {
                        _image.color = Color.red;
                        GameController.GreenBudgieCount--;
                    }
                    else
                    {
                        _image.color = Color.green;
                        GameController.GreenBudgieCount++;
                    }
                }
                break;
            case BudgieType.Blue:
                if (GameController.BlueBudgieCount >= 1)
                {
                    if (FiftyFifty())
                    {
                        _image.color = Color.red;
                        GameController.BlueBudgieCount--;
                    }
                    else
                    {
                        _image.color = Color.green;
                        GameController.BlueBudgieCount++;
                    }
                }
                break;
            case BudgieType.Red:
                if (GameController.RedBudgieCount >= 1)
                {
                    if (FiftyFifty())
                    {
                        _image.color = Color.red;
                        GameController.RedBudgieCount--;
                    }
                    else
                    {
                        _image.color = Color.green;
                        GameController.RedBudgieCount++;
                    }
                }
                break;
            case BudgieType.Gold:
                if (GameController.GoldBudgieCount >= 1)
                {
                    if (FiftyFifty())
                    {
                        _image.color = Color.red;
                        GameController.GoldBudgieCount--;
                    }
                    else
                    {
                        _image.color = Color.green;
                        GameController.GoldBudgieCount++;
                    }
                }
                break;
        }

        Invoke("ResetButton", 0.5f);
        UpdateBirdText();
    }

    private void ResetButton()
    {
        _image.color = Color.white;
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
