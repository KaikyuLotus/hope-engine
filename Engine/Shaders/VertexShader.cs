using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Shaders
{
    class VertexShader : Shader
    {
        public VertexShader(string source) : base(ShaderType.VertexShader, source) { }

        public static Shader FromResources(string fileName)
        {
            return FromResources(ShaderType.VertexShader, fileName);
        }

    }
}