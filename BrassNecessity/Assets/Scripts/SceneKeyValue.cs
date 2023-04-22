using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneKeyValue
{
    [SerializeField]
    private SceneKey key;
    public SceneKey Key
    {
        get => key;
    }
    [SerializeField]
    private string value;
    public string Value
    {
        get => value;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(Value);
    }
}
