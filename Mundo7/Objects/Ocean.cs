using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mundo7.Manager;
using Mundo7.SetRec;


namespace Mundo7.Objects
{
    class Ocean : Obj
    {
        #region Variable
        VertexPositionTexture[] verts;
        int[] indices;
        IndexBuffer iBuffer;
        VertexBuffer vBuffer;
        int row, column;
        Effect effect;
        Matrix world;
        float time = 0;
        #endregion

        public Ocean(string color)
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(10);
            this.world *= Matrix.CreateRotationX(MathHelper.ToRadians(90));
            this.world *= Matrix.CreateTranslation(Vector3.Down * 12);

            this.row = 600;
            this.column = 600;

            this.verts = new VertexPositionTexture[this.row * this.column];

            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.column; j++)
                {
                    this.verts[i * this.column + j] = new VertexPositionTexture(new Vector3((j - this.column / 2f) / 10f, (-i + this.row / 2f) / 10f, 0),
                                                                                new Vector2(j / (float)(this.column - 1), i / (float)(this.row - 1)));
                }
            }

            this.vBuffer = new VertexBuffer(SceneManager.staticDevice,
                                           typeof(VertexPositionTexture),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(this.verts);

            this.indices = new int[(this.row - 1) * (this.column - 1) * 2 * 3];

            int k = 0;
            for (int i = 0; i < this.row - 1; i++)
            {
                for (int j = 0; j < this.column - 1; j++)
                {
                    this.indices[k++] = (int)(i * this.column + j);      // v0
                    this.indices[k++] = (int)(i * this.column + (j + 1)); // v1
                    this.indices[k++] = (int)((i + 1) * this.column + j);      // v2

                    this.indices[k++] = (int)(i * this.column + (j + 1)); // v1
                    this.indices[k++] = (int)((i + 1) * this.column + (j + 1)); // v3
                    this.indices[k++] = (int)((i + 1) * this.column + j);      // v2
                }
            }

            this.iBuffer = new IndexBuffer(SceneManager.staticDevice,
                                           IndexElementSize.ThirtyTwoBits,
                                           this.indices.Length,
                                           BufferUsage.None);
            this.iBuffer.SetData<int>(this.indices);

            this.effect = SceneManager.staticContent.Load<Effect>(@"Effect\Ocean");

            this.texture = SceneManager.staticContent.Load<Texture2D>(@"Texture\" + color);
        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.Milliseconds * 0.0001f;
        }

        public override void Draw(Camera camera)
        {
            this.effect.CurrentTechnique = this.effect.Techniques["Technique1"];
            this.effect.Parameters["World"].SetValue(world);
            this.effect.Parameters["View"].SetValue(camera.GetView());
            this.effect.Parameters["Projection"].SetValue(camera.GetProjection());
            this.effect.Parameters["colorTexture"].SetValue(texture);
            this.effect.Parameters["t"].SetValue(time);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                SceneManager.staticDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, this.verts, 0, this.verts.Length, this.indices, 0, this.indices.Length / 3);
            }

        }
    }
}