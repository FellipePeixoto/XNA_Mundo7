#region a
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Mundo7.SetRec
{
    public class Screen
    {
        private int width;
        private int height;
        private static Screen instance;

        public static Screen GetInstance()
        {
            if (instance == null)
            {
                instance = new Screen();
            }

            return instance;
        }

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public int GetWidth()
        {
            return width;
        }

        public void SetHeight(int height)
        {
            this.height = height;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}
