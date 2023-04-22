using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseMenuButton : GenericButton
{
    public CloseMenuButton(MenuButtonData buttonData, Button button) : base(buttonData, button) { }

    public override void Execute()
    {
        GameObject activatedMenu = menuButtonData.ActivatedMenu;
        GameObject hidMenu = menuButtonData.HidMenu;
        if (activatedMenu != null) 
        { 
            activatedMenu.SetActive(true);
            MenuController controller = MonoBehaviour.FindObjectOfType<MenuController>();
            controller.MenuUI = activatedMenu.GetComponent<MenuUIBehaviour>();
        }
        hidMenu.SetActive(false);
    }
}
