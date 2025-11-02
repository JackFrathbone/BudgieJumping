using UnityEngine;

public class ConfirmMenu : MonoBehaviour
{
    public void Cancel()
    {
        GameController.Unpause();
        gameObject.SetActive(false);
    }
}
