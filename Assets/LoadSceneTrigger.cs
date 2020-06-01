using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField]
        protected string sceneToLoad;
        [SerializeField]
        protected string sceneToUnload;

        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(sceneToUnload);
            Destroy(this);
        }
    }
}