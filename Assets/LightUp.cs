using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class LightUp : MonoBehaviour
    {
        [SerializeField]
        protected Renderer rend;
        [SerializeField, ColorUsage(false, true)] //false: no alpha, true: HDR color.
        protected Color fullIntensity, noIntensity;
        [SerializeField]
        protected string colorProperty;
        [SerializeField]
        protected float maxRange;
        [SerializeField]
        protected Light pointLight;

        int colorID;
        Material mat;
        private void Start()
        {
            mat = rend.material;
            colorID = Shader.PropertyToID(colorProperty);
            mat.SetColor(colorID, noIntensity);
        }

        private void OnTriggerStay(Collider other)
        {
            float dist = Vector3.Distance(other.transform.position, transform.position);
            Color col = Color.Lerp(fullIntensity, noIntensity, dist / maxRange);
            mat.SetColor(colorProperty, col);
            pointLight.intensity = (1 - dist / maxRange) * 2f;
        }
    }
}