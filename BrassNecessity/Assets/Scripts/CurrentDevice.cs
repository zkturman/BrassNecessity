using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentDevice
{
    public static string DeviceScheme { get; set; }
    public static bool IsCurrentDeviceKeyboard()
    {
        return DeviceScheme == "KeyboardMouse";
    }
}
