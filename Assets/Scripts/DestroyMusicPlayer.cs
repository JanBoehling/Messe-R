using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 0)
        {
            Destroy(objs[0]);
        }
    }
}
