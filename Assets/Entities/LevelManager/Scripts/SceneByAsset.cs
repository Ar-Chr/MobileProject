using System;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneByAsset
{
    public string SceneName;
    public string ScenePath;

    public SceneByAsset(string sceneName)
    {
        SceneName = sceneName;
    }

    public Scene Scene => SceneManager.GetSceneByName(SceneName);
}