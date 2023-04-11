using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class StartScreenUIBehaviour : MonoBehaviour
{
    private VisualElement rootVisualElement;
    [SerializeField]
    private MenuButtonData[] allButtonData;
    private Dictionary<string, GenericButton> buttonMap;
    private int currentButton = 0;
    private MenuButtonFactory buttonFactory;

    void OnEnable()
    {
        buttonFactory = new MenuButtonFactory();
        buttonMap = new Dictionary<string, GenericButton>();
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        for (int i = 0; i < allButtonData.Length; i++)
        {
            string buttonName = allButtonData[i].ButtonName;
            Button uiButton = rootVisualElement.Q<Button>(buttonName);
            GenericButton menuButton = buttonFactory.CreateButton(allButtonData[i], uiButton);
            buttonMap.Add(allButtonData[i].ButtonName, menuButton);
        }
    }

    private void Start()
    {
        toggleButtonSelectClass(currentButton);
    }

    public void NavigateToNextButton(int direction)
    {
        int newButtonIndex = (currentButton - direction + allButtonData.Length) % allButtonData.Length; //handles falling off edge using % of length
        updateButtonSelection(newButtonIndex);
    }

    private void updateButtonSelection(int indexToToggle)
    {
        toggleButtonSelectClass(currentButton);
        currentButton = indexToToggle;
        toggleButtonSelectClass(currentButton);
    }

    private void toggleButtonSelectClass(int indexToSelect)
    {
        string buttonName = allButtonData[indexToSelect].ButtonName;
        buttonMap[buttonName].ToggleSelect();
    }

    public void ExecuteCurrentButton()
    {
        string buttonName = allButtonData[currentButton].ButtonName;
        buttonMap[buttonName].Execute();
    }
}
