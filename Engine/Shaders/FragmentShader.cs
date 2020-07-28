using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Shaders
{
    class FragmentShader : Shader
    {
        public FragmentShader(string source) : base(ShaderType.FragmentShader, source) { }

        public static Shader FromResources(string fileName)
        {
            return FromResources(ShaderType.FragmentShader, fileName);
        }

    }
}