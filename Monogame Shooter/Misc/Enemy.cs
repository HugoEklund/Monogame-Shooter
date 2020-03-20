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
    public enum SlimeAnimations
    {
        Move,
        Attack,
        Death
    }
    public class Enemy : EnemyManager
    {
        private int mySpeed;
        SlimeAnimations myAnimation;

        public Texture2D myMoveAnimation;
        public Texture2D myAttackAnimation;
        public Texture2D myDeathAnimation;

        Animation moveAnimation;
        Animation attackAnimation;
        Animation dieAnimation;


        public override void Damage(int someDamage = 10)
        {
            AccessEnemyHp -= someDamage;
        }

        public Enemy(ContentManager Content, GraphicsDeviceManager gdm)
        {
            isAlive = true;
            mySprite = Content.Load<Texture2D>(@"Enemy Sprites\slime");
            myMoveAnimation = Content.Load<Texture2D>(@"Enemy Sprites\moveAnim");
            myAttackAnimation = Content.Load<Texture2D>(@"Enemy Sprites\attackAnim");
            myDeathAnimation = Content.Load<Texture2D>(@"Enemy Sprites\deathAnim");

            myPos = new Vector2();
            moveAnimation = new Animation(myMoveAnimation, 2, 1, 5);
        }

        public override void Initialize()
        {
            myAnimation = SlimeAnimations.Move;
        }

        public override void Update()
        {

            myHitbox = new Rectangle(myPos.ToPoint(), new Point(mySprite.Width, mySprite.Height));
            List<Platform> tempCollidedPlatforms = PlatformManager.Intersects(myHitbox);

            //myPos.X = PlatformManager.tempCollidedPlatforms[i].GetHitBox.Top - myHitbox.Height + 45;
        }

        public override void Draw()
        {

        }
    }
}
