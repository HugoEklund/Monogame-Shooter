﻿using System;
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
    public static class PlatformManager
    {
        static Random myRNG = new Random();
        static Texture2D[] myPlatformTileset;
        static List<Platform> myPlatforms = new List<Platform>();
        static int myBlockWidth;
        static int myBlockHeight;

        public static void Initialize(Texture2D aTexture, int aWidth, int aHeight)
        {
            myPlatformTileset = new Texture2D[aTexture.Width / aWidth * aTexture.Height / aHeight];
            myBlockWidth = aWidth;
            myBlockHeight = aHeight;
            for(int i = 0; i < aTexture.Height / aHeight; i++)
            {
                for(int j = 0; j < aTexture.Width / aWidth; j++)
                {
                    myPlatformTileset[i * aTexture.Width / aWidth + j] = new Texture2D(Game1.graphics.GraphicsDevice, aWidth, aHeight);
                    Color[] tempColorArray = new Color[aWidth * aHeight];
                    aTexture.GetData(0, new Rectangle(j * aWidth, i * aHeight, aWidth, aHeight), tempColorArray, 0, aWidth * aHeight);
                    myPlatformTileset[i * aTexture.Width / aWidth + j].SetData(tempColorArray);
                }
            }
        }

        public static void GeneratePlatforms(int aCount)
        {
            for (int n = 0; n < aCount; n++)
            {

                int tempBlockCountWidth = myRNG.Next(18, 25);
                int tempBlockCountHeight = myRNG.Next(3, 5);
                Texture2D tempPlatformTexture = new Texture2D(Game1.graphics.GraphicsDevice, tempBlockCountWidth * myBlockWidth, tempBlockCountHeight * myBlockHeight);
                for (int i = 0; i < tempBlockCountHeight; i++)
                {
                    for (int j = 0; j < tempBlockCountWidth; j++)
                    {
                        Color[] tempPixelData = new Color[myBlockHeight * myBlockWidth];
                        #region aaaaaah
                        if (i == 0 && j == 0)
                        {
                            myPlatformTileset[0].GetData(tempPixelData);
                        }
                        else if (i == 0 && j > 0 && j < tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[1].GetData(tempPixelData);
                        }
                        else if (i == 0 && j == tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[2].GetData(tempPixelData);
                        }
                        else if (i > 0 && i < tempBlockCountHeight - 1 && j == 0)
                        {
                            myPlatformTileset[3].GetData(tempPixelData);
                        }
                        else if (i > 0 && i < tempBlockCountHeight - 1 && j > 0 && j < tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[4].GetData(tempPixelData);
                        }
                        else if (i > 0 && i < tempBlockCountHeight - 1 && j == tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[5].GetData(tempPixelData);
                        }
                        else if (i == tempBlockCountHeight - 1 && j == 0)
                        {
                            myPlatformTileset[6].GetData(tempPixelData);
                        }
                        else if (i == tempBlockCountHeight - 1 && j > 0 && j < tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[7].GetData(tempPixelData);
                        }
                        else if (i == tempBlockCountHeight - 1 && j == tempBlockCountWidth - 1)
                        {
                            myPlatformTileset[8].GetData(tempPixelData);
                        }
                        #endregion
                        tempPlatformTexture.SetData(0, new Rectangle(j * myBlockWidth, i * myBlockHeight, myBlockWidth, myBlockHeight), tempPixelData, 0, myBlockHeight * myBlockWidth);
                    }
                }
                if(n == 0)
                {
                    myPlatforms.Add(new Platform(tempPlatformTexture, new Point(Game1.graphics.PreferredBackBufferWidth + n * 25 * 20,
                       700)));
                }
                else
                {
                    myPlatforms.Add(new Platform(tempPlatformTexture, new Point(Game1.graphics.PreferredBackBufferWidth + n * 25 * 20,
                        Math.Max(Math.Min(myRNG.Next(myPlatforms[n -1].GetHitBox.Location.Y - 256, myPlatforms[n - 1].GetHitBox.Location.Y + 256), Game1.graphics.PreferredBackBufferHeight - 128), 128))));
                }

                

            }
        }

        public static List<Platform> Intersects(Rectangle playerHitbox)
        {
            List<Platform> tempPlatforms = new List<Platform>();
            for (int i = 0; i < myPlatforms.Count; i++)
            {
                if (myPlatforms[i].GetHitBox.Intersects(playerHitbox))
                {
                    tempPlatforms.Add(myPlatforms[i]);
                }
            }
            return tempPlatforms;
        }
        public static void Update()
        {
            for (int i = 0; i < myPlatforms.Count; i++)
            {          
                myPlatforms[i].Update();
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myPlatforms.Count; i++)
            {
                myPlatforms[i].Draw(aSpriteBatch);
            }
        }
    }
}
