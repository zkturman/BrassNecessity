using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenMenuButton : GenericButton
{
    public OpenMenuButton(MenuButtonData buttonData, Button button) : base(buttonData, button) { }

    public override void Execute()
    {
        GameObject deactivatedMenu = menuButtonData.DeactivatedMenu;
        GameObject unhidMenu = menuButtonData.UnhidMenu;
        deactivatedMenu.SetActive(false);
        unhidMenu.SetActive(true);
        MenuController controller = MonoBehaviour.FindObjectOfType<MenuController>();
        controller.MenuUI = unhidMenu.GetComponent<MenuUIBehaviour>();
    }
}
