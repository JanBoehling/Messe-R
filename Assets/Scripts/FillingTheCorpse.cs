using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FillingTheCorpse : MonoBehaviour
{
    public int counterOfOrgans;
    [SerializeField] GameObject[] organsOfTheCorpse;

    private void Update()
    {
        FillingItWithOrgans();
    }

    private void FillingItWithOrgans()
    {
        if (counterOfOrgans == 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float talkradius = 2.1f;
                Collider[] colliderarray = Physics.OverlapSphere(transform.position, talkradius);
                foreach (Collider collider in colliderarray)
                {
                    Debug.Log("ColliderHit");
                    if (collider.TryGetComponent(out FillingTheCorpse fillingup))
                    {
                        Debug.Log("Component there");
                        StartCoroutine(SpawningThemOrgans());
                    }
                }
            }
        }

    }

    private IEnumerator SpawningThemOrgans()
    {
        for (int i = 0; i <= organsOfTheCorpse.Length -1; i++)
        {
            organsOfTheCorpse[i].SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        GameManager.Instance.WonGame();
        yield return null;
    }




}
