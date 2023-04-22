using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigatorComponent : MonoBehaviour
{
    [SerializeField]
    protected PlayerControllerInputs input;
    protected MenuNavigationControls controlHandler;
    // Start is called before the first frame update
    protected void Start()
    {
        controlHandler = new MenuNavigationControls(input);
    }
}
