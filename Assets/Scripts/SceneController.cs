using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
    public void ExitGame() => Application.Quit();
}
