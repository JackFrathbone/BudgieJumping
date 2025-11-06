using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GambleController : MonoBehaviour
{
    [SerializeField] List<GambleButton> _gambleButtons = new();
    [SerializeField] GameObject _rollButton;

    private void OnEnable()
    {
        Time.timeScale = 1f;
    }

    public void Roll()
    {
        _rollButton.SetActive(false);

        Invoke("RollVisuals", 1f);
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

    private void RollVisuals()
    {
        RollOutcome();
    }

    private void RollOutcome()
    {
        _rollButton.SetActive(true);

        if (FiftyFifty())
        {
            foreach (GambleButton gButton in _gambleButtons)
            {
                gButton.RemoveAllBudgies();
            }
        }
        else
        {
            foreach (GambleButton gButton in _gambleButtons)
            {
                gButton.DoubleBudgies();
            }
        }
    }
}
