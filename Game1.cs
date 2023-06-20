using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using System;

namespace BasicCameraTutorial
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D playerSprite;
        private Texture2D bg;
        private Rectangle rect;
        private Texture2D pixel;

        Player player = new Player();
        Camera camera;
        Canvas canvas;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(_graphics.GraphicsDevice);
            canvas = new Canvas(_graphics.GraphicsDevice, 1280, 720);
            SetResolution(1280, 720);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerSprite = Content.Load<Texture2D>("player");
            bg = Content.Load<Texture2D>("background");
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SetResolution(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
            // TODO: Add your update logic here
            player.Update(gameTime);
            camera.Position = player.Pos;
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            canvas.Activate();
            _spriteBatch.Begin(camera);
            _spriteBatch.Draw(bg, new Vector2(-500, -500), Color.White);
            _spriteBatch.Draw(playerSprite, new Vector2(player.Pos.X - 48, player.Pos.Y - 48), Color.White);
            _spriteBatch.End();

            canvas.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        private void SetResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            Window.IsBorderless = false;
            _graphics.ApplyChanges();
            canvas.SetDestinationRectangle();
        }
    }
}