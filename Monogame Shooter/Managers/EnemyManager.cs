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
    public abstract class EnemyManager
    {
        private int myHealth;
        private int myDamage;
        public abstract void Update();
        public abstract void Draw();

        public int AccessEnemyHp
        {
            get => default(int);
            set
            {
            }
        }

        public int GetEnemyDamage
        {
            get => default(int);
            set
            {
            }
        }
    }
}