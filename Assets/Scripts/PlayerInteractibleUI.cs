using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractibleUI : MonoBehaviour
{
    [SerializeField] GameObject containerGO;
    [SerializeField] InterctableUI interctableUI;
    private void Update()
    {
        if (interctableUI.ItemIneractabel() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        containerGO.SetActive(true);
    }

    private void Hide()
    {
        containerGO.SetActive(false);
    }


}
