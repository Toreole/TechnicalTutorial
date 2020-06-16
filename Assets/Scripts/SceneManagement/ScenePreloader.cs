using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Tutorial
{
    public class ScenePreloader : MonoBehaviour
    {
        [SerializeField]
        protected string[] scenesToLoad;
        [SerializeField]
        protected CanvasGroup faderGroup;
        [SerializeField]
        protected GameObject loadingScreen;
        [SerializeField]
        protected TextMeshProUGUI loadingProgressText;
        [SerializeField]
        protected Slider loadingSlider;
        [SerializeField]
        protected bool activateAllTogether = false;
        [SerializeField]
        protected GameObject pressAnyKeyPrompt;


        [SerializeField]
        protected PlayerController player;

        //! Start can be called as a coroutine!
        IEnumerator Start()
        {
            //we want to preload all the scenes before we start fading in the screen, thats why we need to yield return the coroutine.
            yield return Preload();

            pressAnyKeyPrompt.SetActive(true);
            yield return new WaitUntil(AnyKeyWasPressed);
            pressAnyKeyPrompt.SetActive(false);
            yield return FadeIn();
        }
        
        bool AnyKeyWasPressed()
        {
            return Input.anyKeyDown;
        }

        IEnumerator Preload()
        {
            //make sure the loading screen is all active and opaque.
            faderGroup.alpha = 1;
            loadingScreen.SetActive(true);
            //list of all done operations, only required if activateAllTogether
            List<AsyncOperation> operations = new List<AsyncOperation>();

            //load all the scenes in the array.
            foreach (string scene in scenesToLoad)
            {
                //start the async operation.
                var operation = CustomSceneManager.LoadScene(scene);
                operations.Add(operation);
                //allow scene activation => not activateAllTogether (false => allowSceneActivation = true, vice versa)
                operation.allowSceneActivation = !activateAllTogether;
                //repeat these while it's still loading.
                while (!operation.isDone)
                {
                    //Update the screen.
                    UpdateScreen(operation, scene);
                    //operations get halted at 0.9f because unity.
                    if (activateAllTogether && operation.progress >= 0.9f)
                        break;
                    //next frame.
                    yield return null;
                }
            }
            //all scenes have been loaded, but arent active yet.
            if (activateAllTogether)
            {
                foreach (var op in operations)
                    op.allowSceneActivation = true;
            }
        }

        void UpdateScreen(AsyncOperation op, string sceneName)
        {
            //update the text in our progress bar at the top of the screen.
            loadingProgressText.text = $"{sceneName} - {(op.progress * 100f).ToString("00.0")}%";
            //update the slider.
            loadingSlider.value = op.progress;
        }

        IEnumerator FadeIn()
        {
            //loadingScreen.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            float fadeTime = 1f;
            //count down from 1 to 0 and set the alpha of the canvas group
            for (float t = fadeTime; t > 0f; t -= Time.deltaTime)
            {
                faderGroup.alpha = t / fadeTime;
                yield return null;
            }
            //enable the player controller.
            player.enabled = true;
        }
    }
}