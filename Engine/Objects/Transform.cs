using OpenTK;

namespace HopeEngine.Engine.Objects
{
    public class Transform
    {
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = new Vector3(1, 1, 0);
        public Vector3 Position = Vector3.Zero;

        public Matrix4 CalculateModelMatrix()

        {
            return Matrix4.Scale(Scale)
                * Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation(Position);
        }

        /// <summary>
        /// This works only if the objects coordinates are 0 or 1, which is a standard for the engine
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInside2D(float x, float y)
        {
            float minX = Position.X;
            float minY = Position.Y;
            float maxX = minX + Scale.X;
            float maxY = minY + Scale.Y;
            return (x >= minX && x <= maxX && y >= minY && y <= maxY);
        }

    }
}