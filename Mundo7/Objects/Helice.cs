#region using
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
using Mundo7.SetRec;
#endregion

namespace Mundo7.Objects
{
    class Helice : Obj
    {
        public float rot;

        public Helice(List<Quad> quads, float x, float y, float z)
        {
            this.quads = quads;
            SetPosition(x, y, z);
            rot = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
