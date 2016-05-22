using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong
{
    class Ball
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Direction;
        public Boolean IsActive = false;
        public int Speed = 5;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        public Ball(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Direction = new Vector2(1, 0);
        }

        public void UpdatePosition(Paddle leftPaddle, Paddle rightPaddle)
        {
            if (Bounds.Intersects(rightPaddle.Bounds))
            {
                Direction = CalculateBallDirection(rightPaddle, 270, 1);
                Position.X = rightPaddle.Position.X - Texture.Width - 1;
            }
            else if (Bounds.Intersects(leftPaddle.Bounds))
            {
                Direction = CalculateBallDirection(leftPaddle, 90, -1);
                Position.X = leftPaddle.Position.X + leftPaddle.Texture.Width + 1;
            }

            Position += Direction * Speed;
        }

        private Vector2 CalculateBallDirection(Paddle paddle, int neutralAngle, int multiplyOffset)
        {
            var paddleCenterOffset = (paddle.Position.Y + (float)paddle.Texture.Height / 2 - Position.Y) * multiplyOffset;
            var angle = (paddleCenterOffset * 2) + neutralAngle;
            return VectorMath.AngleToVector(angle);
        }
    }
}
