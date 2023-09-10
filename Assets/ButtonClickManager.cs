using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickManager : MonoBehaviour
{
    public void VisitLink(string link)
    {
        System.Diagnostics.Process.Start(link);
    }
}
