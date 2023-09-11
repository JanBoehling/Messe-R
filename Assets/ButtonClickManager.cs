using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickManager : MonoBehaviour
{
    public void VisitLink(string link)
    {
        if (string.IsNullOrEmpty(link))
            return;

        System.Diagnostics.Process.Start(link);
    }
}
