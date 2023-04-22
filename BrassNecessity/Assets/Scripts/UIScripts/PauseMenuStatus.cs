using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuStatus : MonoBehaviour
{
    [SerializeField]
    private GameObject[] allMenus;

    public bool IsMenuClosed()
    {
        bool anyOpen = false;
        int i = 0;
        while (i < allMenus.Length && !anyOpen)
        {
            anyOpen = allMenus[i].activeSelf;
            i++;
        }
        return !anyOpen;
    }

    public void CloseAll()
    {
        for (int i = 0; i < allMenus.Length; i++)
        {
            allMenus[i].SetActive(false);
        }
    }
}
