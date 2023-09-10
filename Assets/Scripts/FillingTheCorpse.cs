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
    [SerializeField] ParticleSystem particleBlood;

    public void SpawningThemOrgans()
    {

        organsOfTheCorpse[counterOfPresses - 1].SetActive(true);
        particleBlood.Play();

        if (counterOfPresses == maxPresses)
        {
            StartCoroutine(WinningThemGame());
        }
    }

    public IEnumerator WinningThemGame()
    {
        if (counterOfPresses == maxPresses)
        {
            Debug.Log("Corutine läuft");
            yield return new WaitForSeconds(5f);
            GameManager.Instance.WonGame();
        }
        yield return null;
    }
}
