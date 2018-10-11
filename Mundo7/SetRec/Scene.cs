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
using Mundo7.Objects;
#endregion

namespace Mundo7.SetRec
{
    class Scene
    {
        #region private
        List<Obj> objects;
        String desc;
        #endregion

        Obj parede;

        /// <summary>
        /// Cria uma cena
        /// </summary>
        /// <param name="objects">Objetos da cena</param>
        /// <param name="index">Indexador da cena</param>
        /// <param name="desc">Nome da cena</param>
        public Scene(List<Obj> objects, string desc)
        {
            this.objects = objects;
            this.desc = desc;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Obj obj in objects)
            {
                obj.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Obj obj in objects)
            {
                obj.Update(gameTime);
            }
        }

        public void Draw(Camera camera)
        {
            foreach (Obj obj in objects)
            {
                obj.Draw(camera);
            }
        }

        public String GetDescricao()
        {
            return desc;
        }
    }
}
