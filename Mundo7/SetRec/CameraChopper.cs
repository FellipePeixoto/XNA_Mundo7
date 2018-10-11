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
using Mundo7.Manager;
using Mundo7.Objects;
#endregion

namespace Mundo7.SetRec
{
    class CameraChopper : Camera
    {
        public CameraChopper(Vector3 position, Vector3 target)
        {
            this.position = position;
            this.target = target;
            up = Vector3.Up * 2;

            SetupView(this.position, this.target, up);
            boundingBox = new BoundingBox();
            SetupProjection();
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 newPos = SceneManager.chooper.GetCenter() + position;
            SetupView(newPos, target, Vector3.Up);
            SetupProjection();
        }
    }
}
