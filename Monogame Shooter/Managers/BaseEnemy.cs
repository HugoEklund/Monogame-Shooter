using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Shooter
{
    public class BaseEnemy
    {
        private int myHealth;
        private int myDamage;

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

        public void Draw()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}