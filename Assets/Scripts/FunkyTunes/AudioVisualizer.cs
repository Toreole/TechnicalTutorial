using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class AudioVisualizer : MonoBehaviour
    {
        [SerializeField]
        protected AudioSource source;
        [SerializeField]
        protected Transform imageHolder;
        [SerializeField]
        protected GameObject imagePrefab;
        [SerializeField]
        protected float fallOff = 0.2f;
        [SerializeField]
        protected bool useOutputDataInstead = false;
        [SerializeField]
        protected bool useSmoothedValues = true;
        [SerializeField]
        protected bool normalizeValues = false;
        [SerializeField]
        protected bool useLogValues = false;
        [SerializeField]
        protected FFTWindow window;
        [SerializeField, Range(8, samples)]
        protected int shownSamples = 8;

        float[] rawAudio;
        float[] smoothedAudio;
        Image[] images;


        const int samples = 256;

        private void Start() 
        {
            rawAudio = new float[samples];
            smoothedAudio = new float[samples];
            images = new Image[shownSamples];

            for(int i = 0; i<shownSamples; i++)
            {
                images[i] = Instantiate(imagePrefab, imageHolder).GetComponent<Image>();
            }
        }


        private void Update() 
        {
            if(useOutputDataInstead)
                source.GetOutputData(rawAudio, 0);
            else 
                source.GetSpectrumData(rawAudio, 0, window);
           
            if(normalizeValues)
            {
                float value = 0f;
                foreach(float v in rawAudio)
                    value = Mathf.Max(value, v);
                for(int i = 0; i < samples; i++)
                {
                    rawAudio[i] /= value;
                }
            }

            float delta = Time.deltaTime;
            if(useSmoothedValues)
            {
                for(int i = 0; i < shownSamples; i++)
                {
                    smoothedAudio[i] = Mathf.Max(rawAudio[i], smoothedAudio[i] - fallOff * delta);
                    images[i].fillAmount = useLogValues? Mathf.Log(smoothedAudio[i]) : smoothedAudio[i];
                }
            }
            else
            {
                for(int i = 0; i < shownSamples; i++)
                {
                    images[i].fillAmount = useLogValues? Mathf.Log(rawAudio[i]) : rawAudio[i];
                }
            }
        }


    }
}