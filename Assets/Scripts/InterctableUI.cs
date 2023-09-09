using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class InterctableUI : MonoBehaviour
{
    public UnityEvent OnInteractEvent = new UnityEvent();
    [SerializeField] FillingTheCorpse corpseFilled;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float talkradius = 2.1f;
            Collider[] colliderarray = Physics.OverlapSphere(transform.position, talkradius);
            foreach (Collider collider in colliderarray)
            {
                if (collider.TryGetComponent(out PickUpOrgan pickedUp))
                {
                    pickedUp.DestroyObject();
                    GameManager.Instance.AddOrganIcon(pickedUp.Organ);
                    OnInteractEvent?.Invoke();
                }
            }
        }
    }

    public PickUpOrgan ItemIneractabel()
    {
        float talkradius = 2.1f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, talkradius);
        foreach (Collider collider in colliderarray)
        {
            if (collider.TryGetComponent(out PickUpOrgan pickedUp))
            {
                return pickedUp;
            }
        }
        return null;
    }

    public FillingTheCorpse CorpseBeFilled()
    {
        float talkradius = 2.1f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, talkradius);
        foreach (Collider collider in colliderarray)
        {
            if (collider.TryGetComponent(out FillingTheCorpse filledup) && corpseFilled.counterOfOrgans == 5)
            {
                return filledup;
            }
        }
        return null;
    }

}
