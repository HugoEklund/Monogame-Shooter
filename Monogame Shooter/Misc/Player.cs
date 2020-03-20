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
    public class Player
    {
       

        private int myHealth;
        private int myDamage;

        public int AccessPlayerHp
        {
            get => default(int);
            set
            {
            }
        }

        public int GetPlayerDamage
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
    }
}