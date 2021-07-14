using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public static IEnumerator DoLoadCurrentMap()
    {
        return instance.LoadScene(CurrentMap);
    }
    public static IEnumerator DoLoadMainScene()
    {
        return instance.LoadScene(SceneMap0);
    }
    public static IEnumerator DoLoadMap1()
    {
        return instance.LoadScene(SceneMap1);
    }
    public static IEnumerator DoLoadMap2()
    {
        return instance.LoadScene(SceneMap2);
    }
    
    public static IEnumerator UnLoadMap()
    {
        return instance.UnLoadCurrentMap();
    }


    private static SceneHelper instance;
    public static string SceneMap0 => instance.sceneMap0;
    public static string SceneMap1 => instance.sceneMap1;
    public static string SceneMap2 => instance.sceneMap2;
    
    public static string CurrentMap => instance.currentMap;

    public static bool isLoaded { get; private set; }

    [SerializeField] string currentMap;
    [SerializeField] string sceneMap0 = "MainScene";
    [SerializeField] string sceneMap1 = "GameScene";
    [SerializeField] string sceneMap2 = "GameScene2";
    

    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene scene)
    {
        Debug.Log("unloaded: " + scene.name);
        isLoaded = false;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        Debug.Log("loaded: " + scene.name);
        isLoaded = false;
        StartCoroutine(WaitForLoad());
    }

    private IEnumerator WaitForLoad()
    {
        yield return null;
        isLoaded = true;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        currentMap = sceneName;

        if (SceneManager.sceneCount > 1)
        {
            var scene = SceneManager.GetSceneAt(1);
            var sceneUnload = SceneManager.UnloadSceneAsync(scene);

            while (!sceneUnload.isDone)
            {
                yield return null;
            }
        }
        yield return null;

        var sceneload = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!sceneload.isDone)
        {
            yield return null;
        }
        if (SceneManager.sceneCount > 1)
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    private IEnumerator UnLoadCurrentMap()
    {
        if (SceneManager.sceneCount > 1)
        {
            var sceneUnload = SceneManager.UnloadSceneAsync(currentMap);

            while (!sceneUnload.isDone)
            {
                yield return null;
            }
        }
        yield return null;
    }
}