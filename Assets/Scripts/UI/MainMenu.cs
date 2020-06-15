using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        protected string playerScene;
        [SerializeField]
        protected CanvasGroup fader;
        [SerializeField]
        protected float fadeTime = 1f;

        public void GoToGame()
        {
            StartCoroutine(FadeToGame());
        }

        IEnumerator FadeToGame()
        {
            for(float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                //remap t from 0 -> 1
                //essentially make the fader go from fully invisible to fully opaque.
                fader.alpha = t / fadeTime;
                yield return null;
            }
            SceneManager.LoadScene(playerScene);
        }
    }
}