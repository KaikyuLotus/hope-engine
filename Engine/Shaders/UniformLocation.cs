using Android.Graphics;
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

        public UniformLocation UniformVector4(Vector4 vector)
        {
            GL.Uniform4(Location, vector);
            return this;
        }

        public UniformLocation UniformColor(Color color)
        {
            GL.Uniform4(Location, new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f));
            return this;
        }

        public UniformLocation UniformFloat(float value)
        {
            GL.Uniform1(Location, value);
            return this;
        }

    }
}