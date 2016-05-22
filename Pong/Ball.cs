using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    internal class Ball
    {
        public Vector2 Direction;
        public bool IsActive = false;
        public Vector2 Position;
        public int Speed = 5;
        public Texture2D Texture;

        public Ball(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Direction = new Vector2(1, 0);
        }

        public Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height);

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

            Position += Direction*Speed;
        }

        private Vector2 CalculateBallDirection(Paddle paddle, int neutralAngle, int multiplyOffset)
        {
            var halfPaddleWidth = (float) paddle.Texture.Height/2;
            var paddleCenterY = paddle.Position.Y + halfPaddleWidth;
            var ballCenterY = Position.Y + (float) Texture.Height/2;

            var paddleCenterOffset = (paddleCenterY - ballCenterY)*multiplyOffset;

            const int maxAngleOffset = 65;

            // Percentage that the position of the impact was away from the center of the paddle. 100% is the very edge of the paddle.
            var percentageOfMaxAngle = paddleCenterOffset / halfPaddleWidth;

            // Adjust the ball's direction, so that the farther away from the center of the paddle it is, the larger the angle.
            var addedAngle = percentageOfMaxAngle*maxAngleOffset;
            

            var angle = addedAngle + neutralAngle;
            return VectorMath.AngleToVector(angle);
        }
    }
}