using System;
using Microsoft.Xna.Framework;

namespace Pong
{
    class VectorMath
    {
        public float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }

        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle * Math.PI / 180), -(float)Math.Cos(angle * Math.PI / 180));
        }
    }
}
