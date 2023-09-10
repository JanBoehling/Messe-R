using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource cutsceneAudio;

    private AsyncOperation operation;

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(cutsceneAudio.clip.length);
        SceneManager.LoadScene(1);
    }

    public void Skip()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(1);
    }
}
