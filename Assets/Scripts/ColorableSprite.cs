using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [System.Serializable]
    public class ColorableSprite
    {
        [SerializeField]
        protected Sprite baseSprite;
        [SerializeField]
        protected Texture2D mask;
        [SerializeField]
        protected Color mainColor;
        protected Color lastColor;

        protected Texture2D baseTexture;
        protected Texture2D texture;
        protected Sprite resultSprite;
        public Sprite ResultingSprite 
        {
            get 
            {
                if(!resultSprite)
                    Create();
                return resultSprite;
            }
            private set => resultSprite = value;
        }
        
        public Color MainColor 
        {
            get => mainColor; 
            set 
            { 
            mainColor = value; 
            if(mainColor != lastColor) 
                Refresh(); 
            lastColor = mainColor;
            }
        }

        public void Create() 
        {
            CreateTexture();
            Refresh();
        }

        void Refresh()
        {
            for(int x = texture.width -1; x >= 0; x--)
            {
                float relativeX = (float)x / (float)texture.width;
                for(int y = texture.height -1; y >= 0; y--)
                {
                    float relativeY = (float)y / (float)texture.height;
                    //get mask coords
                    int maskX = (int)(relativeX * mask.width);
                    int maskY = (int)(relativeY * mask.height);
                    Color maskTint = mask.GetPixel(maskX, maskY); //only use red channel. should really only be 0 or 1
                    
                    Color col = baseTexture.GetPixel(x, y);
                    texture.SetPixel(x, y, col *= Color.Lerp(Color.white, mainColor, maskTint.r));
                }
            }
            texture.Apply();
        }

        void CreateTexture()
        {
            baseTexture = baseSprite.texture;
            texture = Texture2D.Instantiate(baseTexture);
            var pivot = new Vector2(baseSprite.pivot.x / (float)baseTexture.width, baseSprite.pivot.y / (float)baseTexture.height);
            resultSprite = Sprite.Create(texture, baseSprite.rect, pivot, baseSprite.pixelsPerUnit);
        }

        public void Dispose()
        {
            Object.Destroy(texture);
            Object.Destroy(resultSprite);
        }
    }
}