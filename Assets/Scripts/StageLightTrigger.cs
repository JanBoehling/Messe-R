using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLightTrigger : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var light in lights)
        {
            light.enabled = true;
        }
    }
}
