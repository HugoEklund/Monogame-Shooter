using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Monogame_Shooter
{
    public class Bullets
    {
        // Bullets
        public Texture2D bullet;
        public Vector2 bulletPos;
        float bullSpeed;
        //public Rectangle bulletHitBox;
        public static bool myBullet;
        public bool IsAlive;
        Direction myDirection;

        public Bullets(Vector2 spawnPos, Texture2D aTexture, bool isplayer, Direction aDirection)
        {
            myDirection = aDirection;
            IsAlive = true;
            bullSpeed = 20f;
            bullet = aTexture;
            bulletPos = spawnPos;
            bulletPos.X -= bullet.Width;
            myBullet = isplayer;
        }

        public void Update()
        {
            if (myBullet == true)
            {
                //bulletHitBox = new Rectangle(bulletPos.ToPoint(), new Point(bullet.Width, bullet.Height));

                bulletPos.X += bullSpeed * (myDirection == Direction.Left ? -1 : 1);

                if (bulletPos.X > Game1.graphics.GraphicsDevice.DisplayMode.Width)
                {
                    Game1.mybullets.Remove(this);

                }
            }
            if (myBullet == false)
            {
                //bulletHitBox = new Rectangle(bulletPos.ToPoint(), new Point(bullet.Width, bullet.Height));

                bulletPos.X += bullSpeed;

                if (bulletPos.X > Game1.graphics.GraphicsDevice.DisplayMode.Width)
                {
                    Game1.mybullets.Remove(this);

                }
            }
        }
    }
}