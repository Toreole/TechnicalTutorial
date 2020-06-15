using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [ExecuteInEditMode]
    public class SimpleMaskUpdater : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer provider;
        [SerializeField]
        protected SpriteMask mask;

        // Update is called once per frame
        [ExecuteInEditMode]
        void Update()
        {
            mask.sprite = provider.sprite;
        }
    }
}