using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.Textures;

namespace HopeEngine.Engine.Objects.Geometrics
{
    public class Square : TexturedRenderable
    {
        private static float[] _vertices = new float[]{
            0.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f
        };
        private static float[] _uvs = new float[]{
            0.0f, 1.0f,
            1.0f, 1.0f,
            0.0f, 0.0f,
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f

        };

        public Square(Texture texture, ShaderProgram shader = null) : base(shader ?? Hope.DefaultTexureShader, texture, _vertices, _uvs) { }

    }
}