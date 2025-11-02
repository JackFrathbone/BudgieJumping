using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _tutorialParent;

    [SerializeField] private GameObject _tutorialMovementPrefab;
    [SerializeField] private GameObject _tutorialBudgiePrefab;
    [SerializeField] private GameObject _tutorialRopePrefab;
    [SerializeField] private GameObject _tutorialMarketPrefab;

    [Header("Data")]
    private bool _movementTutShown;
    private bool _budgieTutShown;
    private bool _ropeTutShown;
    private bool _marketTutShown;

    private GameObject _currentTutorial;

    public void ShowMovementTutorial()
    {
        if (_movementTutShown)
            return;

        _movementTutShown = true;
        CreateTutorial(_tutorialMovementPrefab);
    }

    public void ShowBudgieTutorial()
    {
        if (_budgieTutShown)
            return;

        _budgieTutShown = true;
        CreateTutorial(_tutorialBudgiePrefab);
    }

    public void ShowRopeTutorial()
    {
        if (_ropeTutShown)
            return;

        _ropeTutShown = true;
        CreateTutorial(_tutorialRopePrefab);
    }

    public void ShowMarketTutorial()
    {
        if (_marketTutShown)
            return;

        _marketTutShown = true;
        CreateTutorial(_tutorialMarketPrefab);
    }

    private void CreateTutorial(GameObject prefab)
    {
        _currentTutorial = Instantiate(prefab, _tutorialParent);
        _currentTutorial.GetComponentInChildren<Button>().onClick.AddListener(CloseTutorial);

        GameController.Pause();
    }

    public void CloseTutorial()
    {
        if (_currentTutorial == null)
            return;

        _currentTutorial.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        Destroy(_currentTutorial);

        if(!_marketTutShown)
            GameController.Unpause();
    }
}
