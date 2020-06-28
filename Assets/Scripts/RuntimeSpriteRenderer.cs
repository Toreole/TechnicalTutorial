using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
[RequireComponent(typeof(SpriteRenderer))]
    public class RuntimeSpriteRenderer : MonoBehaviour
    {
        [SerializeField]
        protected ColorableSprite sprite;
        [SerializeField]
        protected new SpriteRenderer renderer;
        [SerializeField]
        protected Color color;

        void Start()
        {
            sprite.Create();
            renderer.sprite = sprite.ResultingSprite;
        }

        void Update()
        {
            sprite.MainColor = color;
        }

        private void OnDestroy() 
        {
            sprite.Dispose();
        }
    }
}