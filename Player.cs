using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicCameraTutorial
{
    public class Player
    {
        private Vector2 pos = new Vector2(500, 500);
        private int speed = 720;
        private Vector2 dir;

        public Vector2 Pos { get => pos; set => pos = value; }

        public void Update(GameTime gt)
        {
            dir = Vector2.Zero;
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.W)) dir.Y -= 1;
            if (kState.IsKeyDown(Keys.S)) dir.Y += 1;
            if (kState.IsKeyDown(Keys.A)) dir.X -= 1;
            if (kState.IsKeyDown(Keys.D)) dir.X += 1;

            if (dir != Vector2.Zero) dir.Normalize();

            pos += dir * dt * speed;
            //pos = Vector2.Clamp(pos, new Vector2(200, 200), new Vector2(1300, 1300));
        }
    }
}
