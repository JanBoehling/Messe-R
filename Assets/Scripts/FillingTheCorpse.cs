using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FillingTheCorpse : MonoBehaviour
{
    public int counterOfOrgans;

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
                    if (collider.TryGetComponent(out FillingTheCorpse fillingup))
                    {
                        GameManager.Instance.WonGame();
                    }
                }
            }
        }
    }
    


    
}
