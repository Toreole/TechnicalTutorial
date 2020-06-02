using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    //a static class cant be instantiated, it just exists and does things.
    public static class CustomSceneManager
    {
        //the list of currently loaded scenes. Note that the Player scene is never included in this.
        static List<string> loadedScenes = new List<string>();

        //unload a scene.
        public static void UnloadScene(string sceneName)
        {
            if (loadedScenes.Contains(sceneName)) //only unload it, if it exists.
            {
                SceneManager.UnloadSceneAsync(sceneName);
                loadedScenes.Remove(sceneName);
            }
        }

        //loads the specified scene.
        public static AsyncOperation LoadScene(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if(operation != null)
                loadedScenes.Add(sceneName);
            return operation;
        }

        //Unloads all current scenes and loads the specified one.
        public static AsyncOperation SwitchToScene(string scene)
        {
            foreach (string s in loadedScenes)
            {
                UnloadScene(s);
            }
            loadedScenes.Add(scene);
            return SceneManager.LoadSceneAsync(scene);
        }

        //gets a copy of the loaded scenes list, but as an array. that way its not possible to modify it from outside.
        public static string[] GetLoadedScenes()
        {
            return loadedScenes.ToArray();
        }
    }
}
