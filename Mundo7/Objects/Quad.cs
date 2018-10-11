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
    enum VERTEX_TYPE { Color, Texture}

    class Quad
    {
        #region private
        Matrix world;
        VertexPositionColor[] vertsColor;
        VertexPositionTexture[] vertsTexture;
        VertexBuffer vertexBuffer;
        BasicEffect basicEffect;
        Effect effect;
        GraphicsDevice device;
        Texture2D texture = null;
        Texture2D snowTexture = null;
        VERTEX_TYPE vertType;
        BoundingBox bound;
        float multi = 0;
        bool flag = false;
        #endregion

        /// <summary>
        /// Cria um retangulo formado por 2 triangulos. Comecar do ponto inferior esquerdo
        /// e terminar no ponto inferior direito
        /// </summary>
        /// <param name="device">Graphic Device</param>
        /// <param name="p1">ponto inferior esquerdo</param>
        /// <param name="p2">ponto superior esquerdo</param>
        /// <param name="p3">ponto superior direito</param>
        /// <param name="p4">ponto inferior direito</param>
        public Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color = default(Color))
        {
           world = Matrix.Identity;

            // Incluir em todo construtor;
            device = SceneManager.staticDevice;

            vertsColor = new VertexPositionColor[6];

            if (object.Equals(color, default(Color)))
            {
                vertsColor[0] = new VertexPositionColor(p1, Color.Black);
                vertsColor[1] = new VertexPositionColor(p2, Color.Black);
                vertsColor[2] = new VertexPositionColor(p3, Color.Black);

                vertsColor[3] = new VertexPositionColor(p1, Color.Black);
                vertsColor[4] = new VertexPositionColor(p3, Color.Black);
                vertsColor[5] = new VertexPositionColor(p4, Color.Black);
            }
            else
            {
                vertsColor[0] = new VertexPositionColor(p1, color);
                vertsColor[1] = new VertexPositionColor(p2, color);
                vertsColor[2] = new VertexPositionColor(p3, color);

                vertsColor[3] = new VertexPositionColor(p1, color);
                vertsColor[4] = new VertexPositionColor(p3, color);
                vertsColor[5] = new VertexPositionColor(p4, color);
            }

            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColor), vertsColor.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertsColor);

            basicEffect = new BasicEffect(device);

            vertType = VERTEX_TYPE.Color;
        }

        public Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, String textureName, String snowTextureName = "")
        {
            if (textureName != "")
            {
                texture = SceneManager.staticContent.Load<Texture2D>(textureName);
            }
            if (snowTextureName != "")
            {
                snowTexture = SceneManager.staticContent.Load<Texture2D>(snowTextureName);
            }

            effect = SceneManager.staticEffect;

            world = Matrix.Identity;

            // Incluir em todo construtor;
            device = SceneManager.staticDevice;

            vertsTexture = new VertexPositionTexture[6];

            vertsTexture[0] = new VertexPositionTexture(p1, Vector2.Zero);
            vertsTexture[1] = new VertexPositionTexture(p2, Vector2.UnitY);
            vertsTexture[2] = new VertexPositionTexture(p3, Vector2.One);

            vertsTexture[3] = new VertexPositionTexture(p1, Vector2.Zero);
            vertsTexture[4] = new VertexPositionTexture(p3, Vector2.One);
            vertsTexture[5] = new VertexPositionTexture(p4, Vector2.UnitX);

            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionTexture), vertsTexture.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionTexture>(vertsTexture);

            vertType = VERTEX_TYPE.Texture;
        }

        public void GenerateBounds(Vector3 p1, Vector3 p3)
        {
            bound.Min = p1 - Vector3.One;
            bound.Max = p3 + Vector3.One;
            
        }

        public void Draw(Camera camera)
        {
            if (vertType == VERTEX_TYPE.Texture)
            {         
                device.SetVertexBuffer(this.vertexBuffer);

                this.effect.CurrentTechnique = this.effect.Techniques["Technique1"];
                this.effect.Parameters["World"].SetValue(this.world);
                this.effect.Parameters["View"].SetValue(camera.GetView());
                this.effect.Parameters["Projection"].SetValue(camera.GetProjection());
                this.effect.Parameters["multi"].SetValue(Game1.multi);
                this.effect.Parameters["colorTextureSnow"].SetValue(this.snowTexture);
                this.effect.Parameters["colorTexture"].SetValue(this.texture);

                foreach (EffectPass i in effect.CurrentTechnique.Passes)
                {
                    i.Apply();
                    this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertsTexture, 0, 2);
                }
            }
            else if (vertType == VERTEX_TYPE.Color)
            {
                device.SetVertexBuffer(this.vertexBuffer);

                basicEffect.World = world;
                basicEffect.View = camera.GetView();
                basicEffect.Projection = camera.GetProjection();
                basicEffect.VertexColorEnabled = true;

                foreach (EffectPass i in this.basicEffect.CurrentTechnique.Passes)
                {
                    i.Apply();
                    this.device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertsColor, 0, 2);
                }
            }
        }

        public void Translate(Vector3 move)
        {
            world *= Matrix.CreateTranslation(move);
            bound.Min = vertsTexture[0].Position - Vector3.One;
            bound.Min = vertsTexture[2].Position - Vector3.One;
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
            if (vertType == VERTEX_TYPE.Color)
            {
                vertsColor[0].Color = color;
                vertsColor[1].Color = color;
                vertsColor[2].Color = color;
                vertsColor[3].Color = color;
                vertsColor[4].Color = color;
                vertsColor[5].Color = color;
            }
        }

        public void SetTexture(Texture2D texture, Texture2D snowTexture = null)
        {
            if (this.snowTexture == null)
                if (snowTexture != null)
                    this.snowTexture = snowTexture;

            if (this.texture == null)
                this.texture = texture;
        }
    }
}