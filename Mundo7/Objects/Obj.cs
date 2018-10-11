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
    class Obj
    {
        #region protected
        protected List<Quad> quads;
        protected Vector3 center;
        protected Texture2D texture;
        protected Texture2D snowTexture;
        public BoundingBox boundinBox;
        #endregion

        public Obj()
        {
            quads = new List<Quad>();
        }

        public Obj(String textureName)
        {
            this.quads = new List<Quad>();
            this.texture = SceneManager.staticContent.Load<Texture2D>(textureName);
        }

        public Obj(List<Quad> quads)
        {
            this.quads = quads;
        }

        public Obj(String textureName, List<Quad> quads)
        {
            this.texture = SceneManager.staticContent.Load<Texture2D>(textureName);

            foreach (Quad quad in quads)
            {
                quad.SetTexture(texture);
            }
            this.quads = quads;
        }

        public Obj(String textureName, String snowTextureName, List<Quad> quads)
        {
            this.texture = SceneManager.staticContent.Load<Texture2D>(textureName);
            this.snowTexture = SceneManager.staticContent.Load<Texture2D>(snowTextureName);

            foreach (Quad quad in quads)
            {
                quad.SetTexture(texture, snowTexture);
            }
            this.quads = quads;
        }

        public Obj(String textureName, String snowTextureName)
        {
            this.texture = SceneManager.staticContent.Load<Texture2D>(textureName);
            this.snowTexture = SceneManager.staticContent.Load<Texture2D>(snowTextureName);
            this.quads = new List<Quad>();
        }

        public virtual void Initialize()
        {
 
        }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(Camera camera)
        {
            foreach (Quad quad in quads)
            {
                quad.Draw(camera);
            }
        }

        public void AddQuads(List<Quad> quads)
        {
            if (texture != null)
            {
                foreach (Quad quad in quads)
                {
                    quad.SetTexture(this.texture,this.snowTexture);
                }
            }

            this.quads.AddRange(quads);
        }

        public List<Quad> GetQuads()
        {
            return quads;
        }

        public void SetPosition(float x, float y, float z)
        {
            center = new Vector3(x, y, z);
            Translate(center);
        }

        public Vector3 GetCenter()
        {
            return center;
        }

        /// <summary>
        /// To use only to move on start, not in animations or dinamyc movement
        /// </summary>
        public void Translate(Vector3 move)
        {
            foreach (Quad quad in quads)
            {
                quad.Translate(move);
            }
        }

        /// <summary>
        /// To use only to rotate on start, not in animations or dinamyc rotations
        /// </summary>
        public void Rotate(Vector3 around, float angle)
        {
            foreach (Quad quad in quads)
            {
                quad.Rotate(around, angle);
            }
        }

        public void Move(Vector3 vector)
        {
            center += vector;
        }

        public void SetColor(Color color)
        {
            foreach(Quad quad in quads)
            {
                quad.SetColor(color);
            }
        }

        protected void UpdateBoundingBox()
        {
            this.boundinBox.Min = center - Vector3.One;
            this.boundinBox.Max = this.center + Vector3.One;
        }

        public BoundingBox GetBoudingBox()
        {
            return boundinBox;
        }
    }
}