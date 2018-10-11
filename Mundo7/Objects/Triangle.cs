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
using Mundo7.Manager;
#endregion

namespace Mundo7.Objects
{
    class Triangle
    {
        #region private
        Matrix world;
        VertexPositionColor[] verts;
        VertexBuffer vertexBuffer;
        BasicEffect effect;
        GraphicsDevice device;
        #endregion

        /// <summary>
        /// Caso a cor n seja especificada os vertices tem respectivamente red, green e blue
        /// </summary>
        /// <param name="p1">primeiro ponto</param>
        /// <param name="p2">segundo ponto</param>
        /// <param name="p3">terceiro ponto</param>
        /// <param name="color">Cor do triângulo</param>
        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color = default(Color))
        {
            world = Matrix.Identity;

            // Incluir em todo construtor;
            device = SceneManager.staticDevice;

            verts = new VertexPositionColor[3];

            if (object.Equals(color, default(Color)))
            {
                verts[0] = new VertexPositionColor(p1, Color.Black);
                verts[1] = new VertexPositionColor(p2, Color.Black);
                verts[2] = new VertexPositionColor(p3, Color.Black);
            }
            else
            {
                verts[0] = new VertexPositionColor(p1, color);
                verts[1] = new VertexPositionColor(p2, color);
                verts[2] = new VertexPositionColor(p3, color);
            }

            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(verts);

            effect = new BasicEffect(device);
        }

        public void Draw(CameraFree camera)
        {
            device.SetVertexBuffer(this.vertexBuffer);

            effect.World = world;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.VertexColorEnabled = true;

            foreach (EffectPass i in this.effect.CurrentTechnique.Passes)
            {
                i.Apply();
                this.device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, verts, 0, 1);
            }
        }

        public void Translate(Vector3 move)
        {
            world *= Matrix.CreateTranslation(move);
        }

        public void Rotate(Vector3 arround, float angle)
        {
            world *= Matrix.CreateFromAxisAngle(arround, angle);
        }

        public void SetIdentity()
        {
            world = Matrix.Identity;
        }

        public void SetColor(Color color)
        {
            verts[0].Color = color;
            verts[1].Color = color;
            verts[2].Color = color;
        }
    }
}
