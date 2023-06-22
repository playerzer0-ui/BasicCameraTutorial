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
        private Texture2D pixel;
        private SpriteFont gameFont;

        Player player = new Player();
        Camera camera;
        Canvas canvas;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(_graphics.GraphicsDevice);

            canvas = new Canvas(_graphics.GraphicsDevice, 1280, 720);
            SetResolution(1280, 720);
            _graphics.ApplyChanges();
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
            gameFont = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            //the key
            int w = _graphics.GraphicsDevice.Viewport.Width;
            int h = _graphics.GraphicsDevice.Viewport.Height;
            //to the problem, to handle the camera offset
            int diffW = (w - 1280) / 2;
            int diffH = (h - 720) / 2;

            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            player.Update(gameTime);
            SetResolution(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
            //add the player position with the diffs
            camera.Position = Vector2.Clamp(
                new Vector2(player.Pos.X + diffW, player.Pos.Y + diffH), 
                new Vector2((float)(w * 0.25), (float)(h * 0.10)), 
                new Vector2((float)(w * 0.80) + 120, (float)(h * 0.95) + 700)
                );
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Gray);

            canvas.Activate();
            _spriteBatch.Begin(camera);
            _spriteBatch.Draw(bg, new Vector2(-500, -500), Color.White);
            _spriteBatch.Draw(playerSprite, new Vector2(player.Pos.X - 48, player.Pos.Y - 48), Color.White);
            _spriteBatch.DrawString(gameFont, "res:" + camera.Position, new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(gameFont, "bg:" + camera.Width, new Vector2(100, 140), Color.White);
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