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
    public class Platform
    {
        public Texture2D mySprite;
        public Rectangle myHitbox;

        public Platform(Texture2D aTexture, Point aPosition)
        {
            
            mySprite = aTexture;
            myHitbox = new Rectangle(aPosition, new Point(aTexture.Width, aTexture.Height));

        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(mySprite, myHitbox, Color.White);

        }

        public void Update()
        {
            myHitbox.Location -= new Point(2, 0);
        }
    }
}
