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
    class CameraFree : Camera
    {
        #region private
        public Vector3 CameraPosition
        {
            get
            {
                return position;
            }
        }

        Vector3 oldPosition;

        float speed = 10;
        float angleY = 0;
        float speedY = 100;
        #endregion

        public CameraFree()
        {
            position = Vector3.Zero;
            target = Vector3.Zero;
            up = Vector3.Up * 2;

            SetupView(position, target, up);
            boundingBox = new BoundingBox();
            SetupProjection();
        }

        public CameraFree(Vector3 position, Vector3 target)
        {
            this.position = position;
            this.target = target;
            up = Vector3.Up * 2;

            SetupView(this.position, this.target, up);
            boundingBox = new BoundingBox();
            SetupProjection();
        }

        public override void  Update(GameTime gameTime)
        {
            UpdateBoundingBox();

            Rotation(gameTime);
            Translation(gameTime);

            UpdateBoundingBox();
            view = Matrix.Identity;
            view *= Matrix.CreateRotationY(MathHelper.ToRadians(angleY));
            view *= Matrix.CreateTranslation(position);
            view = Matrix.Invert(view);

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                SetPosition(Vector3.Backward * 5);
                SetTarget(Vector3.Zero);
            }
        }

        private void Rotation(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                position.Y += 0.1f;

            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                position.Y -= 0.1f;
            }
        }

        private void Translation(GameTime gameTime)
        {

            oldPosition = position;
            bool update = false;

            KeyboardState keyState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.X -= (float)Math.Sin(MathHelper.ToRadians(angleY)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * speed;
                position.Z -= (float)Math.Cos(MathHelper.ToRadians(angleY)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * speed;
                update = true;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                position.X += (float)Math.Sin(MathHelper.ToRadians(angleY)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * speed;
                position.Z += (float)Math.Cos(MathHelper.ToRadians(angleY)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * speed;
                update = true;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY + 90)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY + 90)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                update = true;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY - 90)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY - 90)) * 
                    gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                update = true;
            }

            if (keyState.IsKeyDown(Keys.Q))
            {
                this.angleY += this.speedY * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                update = true;
            }
            if (keyState.IsKeyDown(Keys.E))
            {
                this.angleY -= this.speedY * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                update = true;
            }
            if (update)
            {
                this.UpdateBoundingBox();
            }

        }

        public void CameraFollow(Vector3 target)
        {
            SetTarget(target);
        }

        public void RestorePosition(GameTime gameTime)
        {
            this.position = this.oldPosition;
        }
    }
}
