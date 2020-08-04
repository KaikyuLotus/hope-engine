using Android.Graphics;
using HopeEngine.Engine.Views;
using OpenTK;
using OpenTK.Graphics.ES30;
using System;

namespace HopeEngine.Engine
{
    public static class Hope
    {

        public static GameView DefaultView;

        public static Typeface DefaultTypeface;

        public static Color ClearColor = Color.Black;

        public static double FrameTime = 0.0f;

        public static Vector2 ScreenSize;

        public static HopeEngineView EngineView;

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