using Android.Util;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Shaders
{
    public class ShaderProgram
    {
        public readonly int ProgramCode;

        public ShaderProgram(params Shader[] shaders)
        {
            ProgramCode = GL.CreateProgram();
            foreach (Shader shader in shaders)
            {
                GL.AttachShader(ProgramCode, shader.ShaderCode);
                shader.Delete();
            }
            Link();
            Use();
        }

        public ShaderProgram Link()
        {
            Log.Debug("Hope", $"Linking shader program {ProgramCode}");
            GL.LinkProgram(ProgramCode);
            Hope.AssertNoGLError();
            return this;
        }

        public ShaderProgram Use()
        {
            GL.UseProgram(ProgramCode);
            return this;
        }

        public UniformLocation GetUniformLocation(string locationName)
        {
            int location = GL.GetUniformLocation(ProgramCode, locationName);
            return new UniformLocation(locationName, location);
        }

    }
}