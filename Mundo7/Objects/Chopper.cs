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
    enum STATE { parado = 1, ligado = 2, subindo = 3, voando = 4, descendo = 5 }

    class Chopper : Obj
    {
        #region private
        Helice heliceSuperior;
        Helice heliceInferior;
        float rot = 0;
        float move = 0;
        float totalElapse = 0;
        float maxSec = 5;
        Vector3 newCenter;
        STATE actualState = STATE.parado;
        #endregion

        public Chopper(String textureName) : base(textureName) { }
        public Chopper(String textureName, String snowTextureName) : base(textureName,snowTextureName) { }

        public override void Update(GameTime gameTime)
        {
            //TODO: LOGICA DA MAQUINA DE ESTADOS

            totalElapse += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Console.WriteLine(totalElapse);

            if (totalElapse >= maxSec)
            {
                totalElapse = 0;
                actualState++;
                rot = 0;
                move = 0;
            }
            else
            {
                rot += 0.65f;
                move += 0.0001f;
                newCenter = move * Vector3.Up;
            }

            switch (actualState)
            {
                case STATE.parado:

                    break;

                case STATE.ligado:
                    foreach (Quad quad in heliceSuperior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Up, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceSuperior.GetCenter());
                    }
                    foreach (Quad quad in heliceInferior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Right, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceInferior.GetCenter());
                    }
                    break;

                case STATE.subindo:
                    foreach (Quad quad in heliceSuperior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Up, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceSuperior.GetCenter());
                    }
                    foreach (Quad quad in heliceInferior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Right, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceInferior.GetCenter());
                    }
                    foreach (Quad quad in quads)
                    {
                        quad.Translate(newCenter);
                    }

                    Move(newCenter);
                    heliceSuperior.Move(newCenter);
                    heliceInferior.Move(newCenter);
                    break;

                case STATE.voando:
                    foreach (Quad quad in heliceSuperior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Up, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceSuperior.GetCenter());
                    }
                    foreach (Quad quad in heliceInferior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Right, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceInferior.GetCenter());
                    }
                    break;

                case STATE.descendo:
                    newCenter = -newCenter;
                    foreach (Quad quad in heliceSuperior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Up, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceSuperior.GetCenter());
                    }
                    foreach (Quad quad in heliceInferior.GetQuads())
                    {
                        quad.SetIdentity();
                        quad.Rotate(Vector3.Right, MathHelper.ToDegrees(rot));
                        quad.Translate(heliceInferior.GetCenter());
                    }
                    foreach (Quad quad in quads)
                    {
                        quad.Translate(newCenter);
                    }

                    Move(newCenter);
                    heliceSuperior.Move(newCenter);
                    heliceInferior.Move(newCenter);
                    break;

                default:
                    actualState = STATE.parado;
                    break;
            }

            base.Update(gameTime);
        }

        public void SetMainBlade(float x, float y, float z, List<Quad> quads)
        {
            this.quads.AddRange(quads);
            heliceSuperior = new Helice(quads, x, y, z);
        }

        public void SetTailBlade(float x, float y, float z, List<Quad> quads)
        {
            this.quads.AddRange(quads);
            heliceInferior = new Helice(quads, x, y, z);
        }

        public Vector3 GetPosition()
        {
            return newCenter;
        }

        public STATE GetState()
        {
            return actualState;
        }

        public void SetActualState(STATE state)
        {
            actualState = state;
        }

        public void ResetTimer()
        {
            totalElapse = 0;
        }
    }
}
