using UnityEngine.UIElements;

public class MenuButtonFactory
{
    public MenuButtonFactory() { }
    public GenericButton CreateButton(MenuButtonData buttonData, Button button)
    {
        MenuButtonType type = buttonData.Type;
        GenericButton buttonToCreate;
        switch (type)
        {
            case MenuButtonType.Scene:
                buttonToCreate = new SceneButton(buttonData, button);
                break;
            case MenuButtonType.CloseMenu:
                buttonToCreate = new GenericButton(buttonData, button);
                break;
            case MenuButtonType.QuitGame:
                buttonToCreate = new QuitButton(buttonData, button);
                break;
            default:
                throw new System.ArgumentException("Button type " + type.ToString() + " not supported.");
        }
        return buttonToCreate;
    }
}
