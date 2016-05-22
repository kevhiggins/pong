using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Paddle
    {
        public Texture2D Texture;
        public Vector2 Position;
        public PlayerIndex PlayerIndex;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        public Paddle(Texture2D texture, Vector2 position, PlayerIndex playerIndex)
        {
            Texture = texture;
            Position = position;
            PlayerIndex = playerIndex;
        }

        public void UpdatePosition(GraphicsDevice graphicsDevice)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex);

            if (gamePadState.DPad.Down == ButtonState.Pressed)
            {
                Position.Y += 2;
            }

            if (gamePadState.DPad.Up == ButtonState.Pressed)
            {
                Position.Y -= 2;
            }

            if (Position.Y + Texture.Height > graphicsDevice.Viewport.Height)
            {
                Position.Y = graphicsDevice.Viewport.Height - Texture.Height;
            }
            else if (Position.Y < 0)
            {
                Position.Y = 0;
            }
        }
    }
}
