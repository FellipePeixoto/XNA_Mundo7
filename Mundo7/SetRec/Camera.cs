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
    abstract class Camera
    {
        protected Matrix view;
        protected Matrix projection;
        protected Vector3 position;
        protected Vector3 target;
        protected Vector3 up;
        protected BoundingBox boundingBox;

        public abstract void Update(GameTime gameTime);
        public void UpdateBoundingBox()
        {
            boundingBox.Min = position - Vector3.One;
            boundingBox.Max = position + Vector3.One;
        }
        public void SetPosition(Vector3 position)
        {
            this.position = position;
            up = Vector3.Up;

            SetupView(position, target, up);
            SetupProjection();
        }
        public void SetTarget(Vector3 target)
        {
            this.target = target;

            SetupView(position, target, up);
            SetupProjection();
        }
        public void SetupView(Vector3 position, Vector3 target, Vector3 up)
        {
            view = Matrix.CreateLookAt(position, target, up);
        }
        public void SetupProjection()
        {
            Screen telao = Screen.GetInstance();

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                telao.GetWidth() / (float)telao.GetHeight(),
                0.0001f,
                10000);
        }
        public Matrix GetView()
        {
            return view;
        }
        public Matrix GetProjection()
        {
            return projection;
        }
        public Vector3 GetPosition()
        {
            return this.position;
        }
        public BoundingBox GetBoundingBox()
        {
            return this.boundingBox;
        }
    }
}
