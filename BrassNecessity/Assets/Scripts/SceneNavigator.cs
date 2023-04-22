using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField]
    private List<SceneKeyValue> sceneKeyNames;
    static private Dictionary<SceneKey, SceneKeyValue> sceneAccessKeys;
    private static SceneNavigator singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
            sceneAccessKeys = new Dictionary<SceneKey, SceneKeyValue>();
            sceneKeyNames.ForEach(x => sceneAccessKeys.Add(x.Key, x));
        }
        else if (singleton != this)
        {
            GameObject.Destroy(this);
        }
    }

    static public void OpenScene(SceneKey key)
    {
        sceneAccessKeys[key].LoadScene();
    }
}
