using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle audioToggle;

    private void OnEnable()
    {
        audioToggle.enabled = true;
        DataHolder.enableAudio = true;
    }

    public void ToggleAudio(bool value) => DataHolder.enableAudio = value;
}
