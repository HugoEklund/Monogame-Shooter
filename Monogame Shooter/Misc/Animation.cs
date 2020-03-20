using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Monogame_Shooter
{
    public class Animation
    {
        public Texture2D GetCurrentImage { get => myAnimationTileset[myCurrentImage]; }
        Texture2D[] myAnimationTileset;

        int myFrameCount;
        int myCurrentImage;
        int myCurrentFrame;
        public void LoadContent(ContentManager Content)
        {
            
        }

        public Animation(Texture2D aTexture, int aWidth, int aHeight, int frameCount)
        {
            myFrameCount = frameCount;
            myAnimationTileset = new Texture2D[aHeight * aWidth];
            int tempSpriteWidth = aTexture.Width / aWidth;
            int tempSpriteHeight = aTexture.Height / aHeight;
            for (int i = 0; i < aHeight; i++)
            {
                for (int j = 0; j < aWidth; j++)
                {
                    Color[] colorArray = new Color[tempSpriteHeight * tempSpriteWidth];
                    aTexture.GetData(0, new Rectangle(j * tempSpriteWidth, i * tempSpriteHeight, tempSpriteWidth, tempSpriteHeight), colorArray, 0, tempSpriteWidth * tempSpriteHeight);
                    Texture2D tempImage = new Texture2D(Game1.graphics.GraphicsDevice, tempSpriteWidth, tempSpriteHeight);
                    tempImage.SetData(colorArray);
                    myAnimationTileset[i * aWidth + j] = tempImage;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            myCurrentFrame++;
            if(myCurrentFrame >= myFrameCount)
            {
                myCurrentImage++;
                myCurrentFrame = 0;
                if(myCurrentImage >= myAnimationTileset.Length)
                {
                    myCurrentImage = 0;
                }
            }
        }
    }
}
