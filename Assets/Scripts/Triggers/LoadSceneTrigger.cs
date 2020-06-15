using System;
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
        [SerializeField]
        protected bool setActiveScene = false;

        private void OnTriggerEnter(Collider other)
        {
            AsyncOperation operation = CustomSceneManager.LoadScene(sceneToLoad);
            if(operation != null)
                StartCoroutine(WaitForOperation(operation, sceneToLoad));
            CustomSceneManager.UnloadScene(sceneToUnload);
            Destroy(this);
        }

        IEnumerator WaitForOperation(AsyncOperation op, string name)
        {
            while(!op.isDone)
            {
                yield return null;
            }

            op.allowSceneActivation = true;
            int sceneCount = SceneManager.sceneCount;
            for(int i = 0; i < sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == name)
                {
                    SceneManager.SetActiveScene(scene);
                }
            }
        }
    }
}