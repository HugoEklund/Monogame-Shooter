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
    public enum Direction
    {
        Left,
        Right
    }
    public enum PlayerAnimations
    {
        Run,
        Jump,
        Shoot,
        SlashAttack,
        Fall
    }
    public enum GameState
    {
        Game,
        GameOver,
        Paused,
        Win,
        RoundStart,
        RoundEnd
    }
    public class Game1 : Game
    {
        BackgroundManager bg = new BackgroundManager();

        //Graphics
        //Background bg = new Background();
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        // Game
        SpriteFont ammoFont;
        SpriteFont blueWinCounter;
        SpriteFont redWinCounter;
        int frameCheck;
        int canShoot;
        bool blueWin = false;
        bool redWin = false;
        bool PlatformManagerUpdate = true;
        bool AnimationUpdate = true;
        int oldBulletCount;

        //Player
        public Rectangle oldRectangle;
        public Rectangle currentRectangle;
        public Vector2 lastPosition;
        public Texture2D myRunAnimation;
        public Texture2D myShootAnimation;
        public Texture2D myJumpAnimation;
        public Texture2D mySlashAnimation;
        public Texture2D myFallAnimation;
        public static Texture2D myPlayerTexture;
        private Texture2D myEnemy;
        private Texture2D hpBar;
        public static Vector2 playerPos;
        public Vector2 previousPlayerPos;
        private Vector2 speedLeft;
        private Vector2 speedRight;
        public static int ammo = 100;
        public static int playerHp = 10;
        public static int maxHp = 10;
        private bool myJumpFlag = true;
        private bool myShootFlag = false;
        float myJumpForce = 0;
        float myGravity = 0;
        Direction myDirection;
        PlayerAnimations myAnimation;
        GameState state;

        Animation runAnimation;
        Animation jumpAnimation;
        Animation shootAnimation;
        Animation slashAnimation;
        Animation fallAnimation;

        // Gun
        public static List<Bullets> mybullets = new List<Bullets>();
        //public static List<EnemyBullet> enemyBullet = new List<EnemyBullet>();
        public static Texture2D bullet;
        Texture2D[] myGun;
        Vector2 gunBarrelPos;

        Vector2 muzzleFlashPos;
        Texture2D[] muzzleFlashArray;


        // SFX
        private Song music;
        private SoundEffect bulletSfx;

        // Hitbox
        public Rectangle hitBox;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {

            playerPos = new Vector2(650, 500);
            state = GameState.Game;
            myDirection = Direction.Right;
            myAnimation = PlayerAnimations.Run;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg.LoadContent(Content);

            myRunAnimation = Content.Load<Texture2D>(@"Player Sprites\runAnim");
            myJumpAnimation = Content.Load<Texture2D>(@"Player Sprites\jumpAnim");
            mySlashAnimation = Content.Load<Texture2D>(@"Player Sprites\slashAnim");
            myFallAnimation = Content.Load<Texture2D>(@"Player Sprites\fallAnim");
            //myShootAnimation = Content.Load<Texture2D>(@"Player Sprites\shootAnim");

            //myEnemy = Content.Load<Texture2D>(@"Enemy Sprites\enemy");

            PlatformManager.Initialize(Content.Load<Texture2D>(@"Misc Sprites\platform"), 16, 16);
            bullet = Content.Load<Texture2D>(@"Misc Sprites\bullet");
            //redWinCounter = Content.Load<SpriteFont>("redwin");
            //blueWinCounter = Content.Load<SpriteFont>("bluewin");

            //hpBar = Content.Load<Texture2D>("hpbar");
            ammoFont = Content.Load<SpriteFont>("ammo");

            music = Content.Load<Song>(@"SFX\music");
            //bulletSfx = Content.Load<SoundEffect>(@"SFX\bulletSfx");

            MediaPlayer.Volume = 1.0f;
            SoundEffect.MasterVolume = 0.4f;
            //MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
            //bg.LoadContent(Content);


            myGun = new Texture2D[]
            {
                Content.Load<Texture2D>(@"Misc Sprites\gun"),
                Content.Load<Texture2D>(@"Misc Sprites\gunflip")
            };

            muzzleFlashArray = new Texture2D[]
            {
                Content.Load<Texture2D>(@"Misc Sprites\flash1-1"),
                Content.Load<Texture2D>(@"Misc Sprites\flash1-2"),
                Content.Load<Texture2D>(@"Misc Sprites\flash2-1"),
                Content.Load<Texture2D>(@"Misc Sprites\flash2-2"),
            };

            myPlayerTexture = Content.Load<Texture2D>(@"Player Sprites\player");



            fallAnimation = new Animation(myFallAnimation, 2, 1, 5);
            //myAnimation = new Animation(myShootAnimation, 6, 1, 8);
            jumpAnimation = new Animation(myJumpAnimation, 2, 1, 8);
            runAnimation = new Animation(myRunAnimation, 6, 1, 8);
            //slashAnimation = new Animation(mySlashAnimation, 1, 1, 8);



            PlatformManager.GeneratePlatforms(10);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            bg.Update(gameTime);

            if (PlatformManagerUpdate)
            {
                PlatformManager.Update();
            }



            if (myJumpFlag == false)
            {
                if (AnimationUpdate)
                {
                    if (playerPos.Y < previousPlayerPos.Y)
                    {
                        myAnimation = PlayerAnimations.Jump;
                        jumpAnimation.Update(gameTime);
                    }
                    else if (playerPos.Y > previousPlayerPos.Y)
                    {
                        myAnimation = PlayerAnimations.Fall;
                        fallAnimation.Update(gameTime);
                    }
                }

            }
            else
            {
                if (AnimationUpdate)
                {
                    myAnimation = PlayerAnimations.Run;
                    runAnimation.Update(gameTime);
                }
            }

            if (state == GameState.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
                return;
            }
            if (state == GameState.Paused)
            {
                
                MediaPlayer.Pause();
                PlatformManagerUpdate = false;
                AnimationUpdate = false;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    state = GameState.Game;
                    MediaPlayer.Resume();
                    PlatformManagerUpdate = true;
                    AnimationUpdate = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    Exit();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    graphics.ToggleFullScreen();
                }
                return;
            }


            if (state == GameState.Win)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    MediaPlayer.Stop();
                    Exit();
                }
            }
            if (state == GameState.RoundStart)
            {

            }
            if (state == GameState.RoundEnd)
            {

            }

            canShoot++;
            frameCheck++;

            previousPlayerPos = playerPos;

            playerPos += speedLeft;
            playerPos += speedRight;

            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                state = GameState.Paused;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                myDirection = Direction.Left;
                playerPos.X -= 3f;
                speedLeft = new Vector2(-14.0f, 0);
            }
            else
                speedLeft = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.Right))
            {
                myDirection = Direction.Right;
                playerPos.X += 3f;
                speedRight = new Vector2(14.0f, 0);
            }
            else
                speedRight = Vector2.Zero;



            if (keyState.IsKeyDown(Keys.Space) && canShoot >= 10 && ammo >= 1)
            {
                //bulletSfx.Play();
                //mybullets.Add(new Bullets(new Vector2(playerPos.X + (myDirection == Direction.Right ? myPlayerTexture.Width * 3 : 0), playerPos.Y - bullet.Height / 2 + 50), Content.Load<Texture2D>("bullet"), true, myDirection));
                canShoot = 0;
                ammo--;
                myShootFlag = true;
            }
            if (canShoot > 10)
            {
                canShoot = 10;
            }
            for (int i = 0; i < mybullets.Count; i++)
            {
                mybullets[i].Update();
            }
            if (myDirection == Direction.Left)
            {

                gunBarrelPos = new Vector2(playerPos.X - 100, playerPos.Y + 50);
                muzzleFlashPos = gunBarrelPos;
                muzzleFlashPos.X -= muzzleFlashArray[0].Width / 2;
            }
            else
            {
                gunBarrelPos = new Vector2(playerPos.X + myPlayerTexture.Width + 100, playerPos.Y + 50);
                muzzleFlashPos = gunBarrelPos;
                muzzleFlashPos.X += myGun[(int)myDirection].Width;
            }

            muzzleFlashPos.Y -= muzzleFlashArray[0].Height / 10;

            if (keyState.IsKeyDown(Keys.Up) && myJumpFlag)
            {
                myJumpForce = 30f;
                myGravity = 1;
                myJumpFlag = false;
            }
            playerPos.Y -= myJumpForce;
            myJumpForce -= myGravity;
            hitBox = new Rectangle(playerPos.ToPoint(), new Point(myPlayerTexture.Width, myPlayerTexture.Height));
            List<Platform> tempCollidedPlatforms = PlatformManager.Intersects(hitBox);

            for (int i = 0; i < tempCollidedPlatforms.Count; i++)
            {
                if (hitBox.Top >= tempCollidedPlatforms[i].GetHitBox.Top && oldRectangle.Top >= tempCollidedPlatforms[i].GetHitBox.Bottom)
                {
                    playerPos.Y = tempCollidedPlatforms[i].GetHitBox.Bottom;
                    myJumpForce = 0;
                }
                else if (hitBox.Bottom <= tempCollidedPlatforms[i].GetHitBox.Bottom && oldRectangle.Bottom <= tempCollidedPlatforms[i].GetHitBox.Top)
                {
                    myJumpForce = 0;
                    myGravity = 0;
                    myJumpFlag = true;
                    playerPos.Y = tempCollidedPlatforms[i].GetHitBox.Top - hitBox.Height + 45;
                }
                else if (hitBox.Left >= tempCollidedPlatforms[i].GetHitBox.Left && oldRectangle.Left >= tempCollidedPlatforms[i].GetHitBox.Right)
                {
                    playerPos.X = tempCollidedPlatforms[i].GetHitBox.Right;
                }
                else if (hitBox.Right - tempCollidedPlatforms[i].GetSpeed <= tempCollidedPlatforms[i].GetHitBox.Right && oldRectangle.Right - tempCollidedPlatforms[i].GetSpeed <= tempCollidedPlatforms[i].GetHitBox.Left)
                {
                    playerPos.X = tempCollidedPlatforms[i].GetHitBox.Left - myPlayerTexture.Width - 1;
                }
            }
            if (tempCollidedPlatforms.Count == 0)
            {
                myGravity = 1;
                myJumpFlag = false;
            }

            if (playerPos.Y >= graphics.PreferredBackBufferHeight - myPlayerTexture.Height)
            {
                myJumpForce = 0;
                myGravity = 0;
                myJumpFlag = true;
            }

            lastPosition.X = playerPos.X;
            lastPosition.Y = playerPos.Y;
            hitBox = new Rectangle(playerPos.ToPoint(), new Point(myPlayerTexture.Width, myPlayerTexture.Height));
            oldRectangle = hitBox;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //bg.Draw(spriteBatch);

            PlatformManager.Draw(spriteBatch);


            Rectangle bulletRectangle = new Rectangle(0, 0, myGun[(int)myDirection].Width, myGun[(int)myDirection].Height);
            Rectangle sourceRectangle = new Rectangle(0, 0, myPlayerTexture.Width, myPlayerTexture.Height);
            Rectangle muzzleFlashRectangle = new Rectangle(0, 0, muzzleFlashArray[0].Width, muzzleFlashArray[0].Height);
            Vector2 gunOrigin = new Vector2(myGun[(int)myDirection].Width / 2, myGun[(int)myDirection].Height / 2);
            spriteBatch.Draw(myGun[(int)myDirection], gunBarrelPos, sourceRectangle, Color.White, 0, gunOrigin, 2, SpriteEffects.None, 0);

            Vector2 origin = new Vector2(0, 0);

            if (myAnimation == PlayerAnimations.Run)
            {
                spriteBatch.Draw(runAnimation.GetCurrentImage, playerPos, Color.White);
            }
            if (myAnimation == PlayerAnimations.Jump)
            {
                spriteBatch.Draw(jumpAnimation.GetCurrentImage, playerPos, Color.White);
            }
            if (myAnimation == PlayerAnimations.Shoot)
            {
                spriteBatch.Draw(shootAnimation.GetCurrentImage, playerPos, Color.White);
            }
            if (myAnimation == PlayerAnimations.SlashAttack)
            {
                spriteBatch.Draw(slashAnimation.GetCurrentImage, playerPos, Color.White);
            }
            if (myAnimation == PlayerAnimations.Fall)
            {
                spriteBatch.Draw(fallAnimation.GetCurrentImage, playerPos, Color.White);
            }

            for (int i = 0; i < mybullets.Count; i++)
            {
                if (myDirection == Direction.Left)
                {
                    spriteBatch.Draw(bullet, mybullets[i].bulletPos, bulletRectangle, Color.White, 0, gunOrigin, 2, SpriteEffects.None, 0);

                }
                else
                {
                    spriteBatch.Draw(bullet, mybullets[i].bulletPos, bulletRectangle, Color.White, 0, gunOrigin, 2, SpriteEffects.None, 0);
                }
            }

            if (mybullets.Count > oldBulletCount)
            {
                if (myDirection == Direction.Left)
                {
                    spriteBatch.Draw(muzzleFlashArray[frameCheck % muzzleFlashArray.Length], muzzleFlashPos, muzzleFlashRectangle, Color.White, 0, gunOrigin, 0.2f, SpriteEffects.None, 0);

                }
                else
                {
                    spriteBatch.Draw(muzzleFlashArray[frameCheck % muzzleFlashArray.Length], muzzleFlashPos, muzzleFlashRectangle, Color.White, 0, gunOrigin, 0.2f, SpriteEffects.FlipHorizontally, 0);
                }
            }

            if (state == GameState.Paused)
            {
                spriteBatch.DrawString(ammoFont, "\n\nPress Enter to Resume, Backspace to Quit\nPress M to toggle Fullscreen", new Vector2(20, 10), Color.White);
            }
            if (state == GameState.GameOver)
            {
                spriteBatch.DrawString(ammoFont, "You have died you filthy cretin \nPress Enter to Quit", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.White);
            }
            if (state == GameState.Win)
            {
                if (redWin == true)
                {
                    spriteBatch.DrawString(ammoFont, "Red Team Won \nPress Enter to Quit OR Press H for Hard Mode", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.White);
                }
                if (blueWin == true)
                {
                    spriteBatch.DrawString(ammoFont, "Blue Team Won! \nPress Enter to Quit OR Press H for Hard Mode", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.White);
                }
            }
            if (state == GameState.RoundStart)
            {
                spriteBatch.DrawString(ammoFont, "Defuse the bomb - or kill the enemy - before the timer runs out.", new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Color.White);
            }
            //if (state == gamestate.roundend)
            //{
            //    if(redwin == true)
            //    {
            //        
            //    }
            //    if(bluewin == true)
            //    {
            //        
            //    }
            //    spritebatch.drawstring(ammofont, winningteam + "won the round!", new vector2(graphics.preferredbackbufferwidth / 2, graphics.preferredbackbufferheight / 2), color.white);
            //}
            if(myJumpFlag == false)
            {
                
            }
            else
            {

            }

            // Ammo counter
            spriteBatch.DrawString(ammoFont, "Ammo: " + ammo, new Vector2(20, 100), Color.White);
            //spriteBatch.DrawString(redWinCounter, "Red Team Score: " + redWinCounter, new Vector2(150, 50), Color.White);
            //spriteBatch.DrawString(blueWinCounter, "Blue Team Score: " + blueWinCounter, new Vector2(150, 60), Color.White);

            // Hp bar
            //spriteBatch.Draw(hpBar, new Rectangle((int)playerPos.X, (int)playerPos.Y + myPlayer.Height, (int)(myPlayer.Width * ((float)playerHp / (float)maxHp)), 20), Color.Green);



            spriteBatch.End();
            oldBulletCount = mybullets.Count();

            base.Draw(gameTime);
        }
    }
}