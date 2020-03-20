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
    public class BackgroundManager
    {
        public Texture2D textureBg;
        public Vector2 bgLayer1, bgLayer2, bgLayer3, bgLayer4;
        public int speedBg;

        public BackgroundManager()
        {
            bgLayer1 = new Vector2(500, 500);
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {
            textureBg = Content.Load<Texture2D>("background");

        }

        //Draw
        public void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(textureBg, bgLayer1, Color.White);
            SpriteBatch.Draw(textureBg, bgLayer2, Color.White);
            SpriteBatch.Draw(textureBg, bgLayer3, Color.White);
            SpriteBatch.Draw(textureBg, bgLayer4, Color.White);
        }

        // Update
        public void Update(GameTime gameTime)
        {
            
        }
    }
}
