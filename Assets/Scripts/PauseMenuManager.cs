using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void TogglePauseMenu() => pauseMenu.SetActive(!pauseMenu.activeSelf);
}
