using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Player Stats")]
    public static int playerMoney = 5;

    private void Start()
    {
        DisableCursor();
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
