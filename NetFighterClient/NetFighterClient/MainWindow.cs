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

namespace NetFighterClient
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainWindow : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        //private Texture2D _pixel;
        private SpriteFont _font;
        static private Texture2D _emptyTexture;

        public int Screenwidth = 1280;
        public int Screenheight = 720;

        private PlayerShip _playerObj;
        public List<GameObject> _permanentObjects;

        public bool GravityEnabled { get; set; }
        TimeSpan timer = TimeSpan.Zero;
        public Dictionary<String, bool> _buttonList;



        public MainWindow()
        {
            //_playerObj = new GameObject();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = Screenwidth;
            _graphics.PreferredBackBufferHeight = Screenheight;
            GravityEnabled = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _permanentObjects = new List<GameObject>();
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _buttonList = new Dictionary<string, bool>();
            _buttonList.Add("gravity", false);

        //    _pixel = Content.Load<Texture2D>("pixel"); // change these names to the names of your images
            _font = Content.Load<SpriteFont>("StandardFont"); // Use the name of your font here instead of 'Score'.
            _emptyTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _emptyTexture.SetData(new[] { Color.White });
            _playerObj = new PlayerShip("shuttle", Content, this) { RemoveWhenOutOfBounds = false };
            _permanentObjects.Add(_playerObj);
         
           // _playerObj.Init(Content,);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            
            if(Keyboard.GetState().IsKeyDown(Keys.G) && !_buttonList["gravity"]){
                GravityEnabled = !GravityEnabled;
                _buttonList["gravity"] = true;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.G))
            {
                _buttonList["gravity"] = false;
            }


            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.X))
                this.Exit();


            for (int i = _permanentObjects.Count() - 1; i >= 0; i--)
            {
                _permanentObjects[i].Update(gameTime);
            }
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
           // _spriteBatch.DrawString(_font, "NetFighterClient", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_font, _playerObj.Description, new Vector2(10, 30), Color.White);
            _spriteBatch.DrawString(_font, "Gametime:       " + gameTime.ElapsedGameTime + "\nGametime total: " + gameTime.TotalGameTime + "\nObjects: " + _permanentObjects.Count + "\nGravity: " + GravityEnabled, new Vector2(800, 30), Color.White);
  
            for (int x = 15; x < Screenwidth; x += 15)
                for (int y = 15; y < Screenheight; y += 15)
                    _spriteBatch.Draw(_emptyTexture, new Vector2(x, y), Color.White);

            foreach (GameObject gameObject in _permanentObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
