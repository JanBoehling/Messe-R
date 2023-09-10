using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) TogglePauseMenu();
    }


    public void TogglePauseMenu()
    {
        bool isActive = pauseMenu.activeSelf;
        Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
        pauseMenu.SetActive(!isActive);
    }
}
