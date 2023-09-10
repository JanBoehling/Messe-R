using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform organTracker;

    [SerializeField] private Sprite[] organIconsWhite;
    [SerializeField] private Sprite[] organIconsColored;
    private readonly OrganType[] organTypes = new OrganType[5];

    public void AddOrgan(OrganType organType, int index)
    {
        if (index >= organTracker.childCount) return;
        
        organTracker
            .GetChild(index)
            .GetComponent<Image>().sprite = organIconsWhite[(int)organType];

        organTypes[index] = organType;

        if (index != organTracker.childCount - 1) return;

        for (int i = 0; i < organTracker.childCount; i++)
        {
            organTracker
            .GetChild(i)
            .GetComponent<Image>().sprite = organIconsColored[(int)organTypes[i]];
        }
    }
}
