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
    class Grid : Obj
    {
        #region Variable
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        int[] indices;
        IndexBuffer iBuffer;
        //Texture2D texture;
        Texture2D textureSnow;
        int row, column;
        Effect effect;
        Matrix world;
        Texture2D texHM;
        #endregion

        public Grid(string bumpMap, string snow, string color)
        {
            this.row = 600;
            this.column = 600;

            this.texHM = SceneManager.staticContent.Load<Texture2D>(@"Texture\"+bumpMap);

            Color[] tAux = new Color[texHM.Width * texHM.Height];
            texHM.GetData<Color>(tAux);


            this.verts = new VertexPositionTexture[row * column];

            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.column; j++)
                {

                    float u = j/(float)(column-1);
                    float v = i / (float)(row - 1);

                    int _j = (int)(u * (texHM.Width-1));
                    int _i = (int)(v * (texHM.Height-1));
                    int _Y = _i * texHM.Width + _j;
                     
                    verts[i * column + j] = new VertexPositionTexture(new Vector3(j - column/2f, tAux[_Y].B/10f,i - row/2f), new Vector2(u,v));
                }
            }

            this.vBuffer = new VertexBuffer(SceneManager.staticDevice, typeof(VertexPositionColorTexture), this.verts.Length, BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(this.verts);

            this.indices = new int[row * column * 2 * 3];

            int k = 0;
            for (int i = 0; i < this.row-1; i++)
            {
                for (int j = 0; j < this.column-1; j++)
                {
                    // 1o triangulo
                    this.indices[k++] = (int)( i      * column +  j); // V0
                    this.indices[k++] = (int)( i      * column + (j + 1)); // V1
                    this.indices[k++] = (int)((i + 1) * column + j); // V2
                                         
                    // 2o triangulo      
                    this.indices[k++] = (int)(i * column + (j + 1)); // V1
                    this.indices[k++] = (int)((i + 1) * column + (j+ 1)); // V1
                    this.indices[k++] = (int)((i + 1) * column + j); // V2

                }
            }

            this.iBuffer = new IndexBuffer(SceneManager.staticDevice, IndexElementSize.ThirtyTwoBits, this.indices.Length, BufferUsage.None);
            this.iBuffer.SetData<int>(this.indices);

            this.texture = SceneManager.staticContent.Load<Texture2D>(@"Texture\"+color);
            this.textureSnow = SceneManager.staticContent.Load<Texture2D>(@"Texture\"+snow);

            this.effect = SceneManager.staticContent.Load<Effect>(@"Effect\Effect1");

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateTranslation(Vector3.Down * 27);
        }

        public override void Draw(Camera camera)
        {
            this.effect.CurrentTechnique = this.effect.Techniques["Technique1"];
            this.effect.Parameters["World"].SetValue(world);
            this.effect.Parameters["View"].SetValue(camera.GetView());
            this.effect.Parameters["Projection"].SetValue(camera.GetProjection());
            this.effect.Parameters["colorTexture"].SetValue(texture);
            this.effect.Parameters["colorTextureSnow"].SetValue(this.textureSnow);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                SceneManager.staticDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, this.verts, 0, this.verts.Length, this.indices, 0, this.indices.Length / 3);
            }
            
        }
    }
}
