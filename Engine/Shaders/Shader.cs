using Android.Util;
using OpenTK.Graphics.ES30;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HopeEngine.Engine.Shaders
{
    public class Shader
    {
        public readonly int ShaderCode;

        public Shader(ShaderType shaderType, string source)
        {
            Log.Debug("Hope", $"Creating a new {shaderType}");
            ShaderCode = GL.CreateShader(shaderType);

            Log.Debug("Hope", $"Reading {shaderType} source to shader {ShaderCode}");
            GL.ShaderSource(ShaderCode, 1, new[] { source }, (int[])null);
            Log.Debug("Hope", $"Compiling {shaderType} {ShaderCode}");
            GL.CompileShader(ShaderCode);
            Hope.AssertNoGLError();

            CheckShaderError();
            Log.Debug("Hope", $"{shaderType} created with no errors");
        }

        public Shader Delete()
        {
            Log.Debug("Hope", $"Linking shader program {ShaderCode}");
            GL.DeleteShader(ShaderCode);
            return this;
        }

        private void CheckShaderError()
        {
            int length;
            int compiled;
            GL.GetShader(ShaderCode, ShaderParameter.CompileStatus, out compiled);
            if (compiled == 0)
            {
                GL.GetShader(ShaderCode, ShaderParameter.InfoLogLength, out length);
                if (length > 0)
                {
                    StringBuilder log = new StringBuilder(length);
                    GL.GetShaderInfoLog(ShaderCode, length, new int[] { length }, log);

                    throw new InvalidOperationException("GL2 : Couldn't compile shader: " + log.ToString());
                }

                GL.DeleteShader(ShaderCode);
                throw new InvalidOperationException("Unable to compile shader of type : ?");
            }
        }

        protected static Shader FromResources(ShaderType shaderType, string fileName)
        {
            Log.Info("Hope", $"Creating a new Shader from resources file named {fileName}");

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(Shader)).Assembly;

            using Stream stream = assembly.GetManifestResourceStream(fileName);
            using StreamReader reader = new StreamReader(stream);

            return new Shader(shaderType, reader.ReadToEnd());
        }
    }
}