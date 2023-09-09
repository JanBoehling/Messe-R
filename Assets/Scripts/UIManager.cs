using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform organTracker;

    public void AddOrgan(Sprite sprite, int index)
    {
        if (index >= organTracker.childCount) return;

        organTracker
            .GetChild(index)
            .GetComponent<Image>().sprite = sprite;
    }
}
