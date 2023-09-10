using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FillingTheCorpse : MonoBehaviour
{
    public int counterOfOrgans;
    [SerializeField] GameObject[] organsOfTheCorpse;
    public int counterOfPresses;
    public int maxPresses = 5;

    public void SpawningThemOrgans()
    {
        organsOfTheCorpse[counterOfPresses -1].SetActive(true);
        if (counterOfPresses == maxPresses)
        {
            GameManager.Instance.WonGame();
        }
    }
}
