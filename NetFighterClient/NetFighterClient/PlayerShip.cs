using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameHelper;

namespace NetFighterClient
{
    class PlayerShip : GameObject
    {
        public PlayerShip(string texturename,ContentManager  content, MainWindow mw) : base(texturename,content,mw) {}

        internal override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Angle += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Angle -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Speed += 0.1f * Direction;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Speed -= 0.1f * Direction;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Speed = new Vector2(0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Angle = 0f;
                Speed = new Vector2(0, 0);
                Location = new Vector2(TheGame.Screenwidth / 2f, TheGame.Screenheight / 2f);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Fire();
            }


            base.Update(gameTime);
        }

        private void Fire()
        {
            
            GameObject go = new GameObject("ball", _content, TheGame);
            go.Speed = Direction * 20.0f + Speed;
            go.Location = Location + new Vector2(
                VectorHelper.GetXValue((float)(Angle - Math.PI / 2)),
                VectorHelper.GetYValue((float)(Angle - Math.PI / 2))
                ) * 25.0f;
            go.RemoveWhenOutOfBounds = true;
            TheGame._permanentObjects.Add(go);


            GameObject go2 = new GameObject("ball", _content, TheGame);
            go2.Speed = Direction * 20.0f+ Speed;

            go2.Location = Location + new Vector2(
                VectorHelper.GetXValue((float)(Angle + Math.PI / 2)),
                VectorHelper.GetYValue((float)(Angle + Math.PI / 2))
                ) * 25.0f;
            go2.RemoveWhenOutOfBounds = true;
            TheGame._permanentObjects.Add(go2);
        }
    }
}
