using Android.Graphics;
using HopeEngine.Engine.Views;
using HopeEngine.Engine.Shaders;
using OpenTK;
using OpenTK.Graphics.ES30;
using System;

namespace HopeEngine.Engine
{
    public static class Hope
    {

        public static GameView DefaultView;

        public static ShaderProgram DefaultShader;

        public static ShaderProgram DefaultTexureShader;

        public static ShaderProgram DefaultTextShader;

        public static Typeface DefaultTypeface;

        public static Color ClearColor = Color.Black;

        public static float FrameTime = 0.0f;

        public static Vector2 ScreenSize;


        public static void AssertNoGLError()
        {
            ErrorCode error = GL.GetErrorCode();
            if (error != ErrorCode.NoError)
            {
                throw new Exception(error.ToString());
            }
        }

    }
}