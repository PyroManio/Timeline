using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
public class MultiSceneLoader : MonoBehaviour
{
    int[] mainFlags;
    public static event Action<int[]> LoadFlags;
    private Dictionary<SceneName,string> nameToScene = new Dictionary<SceneName, string>
    {
        { SceneName.Bedroom, "Bedroom" },
        {SceneName.Hallway, "Hallway"}
    };

 void Start()
    {
        foreach (string scene in nameToScene.Values) SceneManager.UnloadSceneAsync(scene);
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        LoadScene(SceneName.Bedroom);
    }

    public void LoadScene(SceneName sceneName){
        Debug.Log("Call to Load Scene");
        
        if (!SceneManager.GetSceneByName(nameToScene[sceneName]).isLoaded)
        {
            Debug.Log("Attempting to load scene: " + sceneName);
            SceneManager.LoadScene(nameToScene[sceneName], LoadSceneMode.Additive);
            LoadFlags?.Invoke(mainFlags);
        }   
    } 
}
public enum SceneName{
    Bedroom,
    UI,
    Hallway
    }