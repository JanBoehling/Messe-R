using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSight : MonoBehaviour
{

    [SerializeField] float maxDistance;

    public bool HasHit()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance);


        if (hit.collider)
        {
            return hit.transform.CompareTag("Player");
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance);
    }
}
