using OpenTK;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Shaders
{
    public class UniformLocation
    {

        public readonly string Name;
        public readonly int Location;

        public UniformLocation(string name, int location)
        {
            Name = name;
            Location = location;
        }

        public UniformLocation UniformMatrix4(Matrix4 matrix)
        {
            GL.UniformMatrix4(Location, false, ref matrix);
            return this;
        }

        public UniformLocation UniformVector3(Vector3 vector)
        {
            GL.Uniform3(Location, vector);
            return this;
        }

    }
}