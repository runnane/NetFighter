using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NetFighterClient
{
    public class GameObject
    {
        internal string Texturename;
        internal float _angle;
        internal MainWindow TheGame;
        internal ContentManager _content;

        internal Texture2D _texture;

        public Vector2 Location { get; set; }
        public Vector2 Speed { get; set; }
        public bool RemoveWhenOutOfBounds { get; set; }
        public float SpeedAngle { get { return (float)Math.Atan(Speed.X / Speed.Y * -1); } }
        public float SpeedLength { get { return (float)Math.Abs(Speed.Y / Math.Sin(SpeedAngle)); } }
        public float Angle { get { return _angle; } set { _angle = MathHelper.WrapAngle(value); } }
        internal Vector2 CenterPoint { get { return new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f); } }
        internal Rectangle BoxObject { get { return new Rectangle(0, 0, _texture.Width, _texture.Height); } }
        internal Vector2 Direction
        {
            get
            {
                var v = new Vector2();
                v.X = (float)Math.Sin(Angle);
                v.Y = (float)Math.Cos(Angle)*-1;

                return v;
            }
        }

        public GameObject(string texturename, ContentManager content, MainWindow mw)
        {
            _content = content;
            Texturename = texturename;
            RemoveWhenOutOfBounds = true;
            TheGame = mw;
            _texture = content.Load<Texture2D>(Texturename);
            Angle = 0f;
            Speed = new Vector2(0, 0);
            Location = new Vector2(TheGame.Screenwidth / 2f, TheGame.Screenheight / 2f);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, BoxObject, Color.White, Angle, CenterPoint, 1.0f, SpriteEffects.None, 1);
        }

        public string Description { 
            get {
                return "obj-angle=" + MathHelper.ToDegrees(Angle) + "\nobj-direction=" + Direction + "\nobj-speed=" + Speed + "\nobj-location=" + Location + "\nspeed-angle=" + MathHelper.ToDegrees(SpeedAngle) + "\nspeed-length=" + SpeedLength; 
            } 
        }


        public bool ShouldBeRemoved()
        {
            if (!RemoveWhenOutOfBounds)
                return false;
            if (Location.X < 0 || Location.X > TheGame.Screenwidth || Location.Y < 0 || Location.Y > TheGame.Screenheight)
                return true;
            return false;
        }

        internal virtual void Update(GameTime gameTime)
        {
            // Gravity
            if (TheGame.GravityEnabled)
            {
                Speed += 0.03f * new Vector2(0, 0.981f);
            }

            // Move object
            Location += Speed;

            // Remove if out of bounds
            if (ShouldBeRemoved())
            {
                TheGame._permanentObjects.Remove(this);
            }

        }
    }
}
