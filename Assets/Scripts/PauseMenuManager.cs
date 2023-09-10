using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) TogglePauseMenu();
    }

    public void TogglePauseMenu() => pauseMenu.SetActive(!pauseMenu.activeSelf);
}
