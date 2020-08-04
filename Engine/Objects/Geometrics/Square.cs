using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.Textures;

namespace HopeEngine.Engine.Objects.Geometrics
{
    public class Square : TexturedRenderable
    {
        public static readonly float[] Vertices = new float[]{
            0.0f, 0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f
        };
        public static readonly float[] Uvs = new float[]{
            0.0f, 1.0f,
            1.0f, 1.0f,
            0.0f, 0.0f,
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f

        };

        public Square(Texture texture, ShaderProgram shader = null) : base(shader ?? HopeShaders.DefaultTexureShader, texture, Vertices, Uvs) { }

    }
}