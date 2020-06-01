using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    protected PlayerController player;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return Preload();
        yield return FadeIn();
    }

    IEnumerator Preload()
    {
        faderGroup.alpha = 1;
        loadingScreen.SetActive(true);
        foreach(string scene in scenesToLoad)
        {
            var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;
            //repeat these while it's still loading.
            while(!operation.isDone)
            {
                loadingProgressText.text = $"{scene} - {(operation.progress * 100f).ToString("00.0")}%";
                loadingSlider.value = operation.progress;

                yield return null;
            }
        }
    }

    IEnumerator FadeIn()
    {
        loadingScreen.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for(float t = 1f; t > 0f; t -= Time.deltaTime)
        {
            faderGroup.alpha = t;
            yield return null;
        }
        player.enabled = true;
    }
}
