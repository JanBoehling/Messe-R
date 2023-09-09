using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterctableUI : MonoBehaviour
{
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

}
