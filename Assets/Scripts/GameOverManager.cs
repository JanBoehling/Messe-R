using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    private void OnEnable()
    {
        messageText.text = DataHolder.GameOverMessage;
        Cursor.lockState = CursorLockMode.None;
    }

    
}
