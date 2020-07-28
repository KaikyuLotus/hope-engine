using Android.Content;
using Android.Util;
using HopeEngine.Engine.Views;
using HopeEngine.Engine.Shaders;
using OpenTK;
using OpenTK.Graphics.ES30;
using OpenTK.Platform.Android;
using System;
using HopeEngine.Engine.Objects;
using HopeEngine.Engine.Scenes;
using System.Diagnostics;
using Android.Views;

namespace HopeEngine.Engine
{

    public class HopeEngineView : AndroidGameView
    {

        private Scene scene;

        private Stopwatch stopwatch = new Stopwatch();

        public HopeEngineView(Context context, Scene scene) : base(context)
        {
            this.scene = scene;
        }

        public void ChangeScene(Scene scene)
        {
            // TODO do something like transitions ?
            this.scene = scene;
            this.scene.Setup();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Run();
        }

        private void LoadStandardSettings()
        {
            try
            {
                // the default GraphicsMode that is set consists of (16, 16, 0, 0, 2, false)
                Log.Debug("Hope", "Loading with default settings");
                base.CreateFrameBuffer();
            }
            catch (Exception ex)
            {
                Log.Error("Hope", "{0}", ex);
                throw ex;
            }
        }

        private void Clear()
        {
            GL.ClearColor(Hope.ClearColor.R, Hope.ClearColor.G, Hope.ClearColor.B, Hope.ClearColor.A);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
        }

        protected override void CreateFrameBuffer()
        {
            Log.Info("Hope", "Creating frame buffer");
            ContextRenderingApi = OpenTK.Graphics.GLVersion.ES3;
            LoadStandardSettings();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Width = Resources.DisplayMetrics.WidthPixels;
            Height = Resources.DisplayMetrics.HeightPixels;
            Hope.ScreenSize = new Vector2(Width, Height);

            Hope.DefaultView = GameView.DefaultView(Width, Height);

            // Will be removed
            Log.Info("Hope", "Creating Shader programs");
            Hope.DefaultShader = new ShaderProgram(
                VertexShader.FromResources("HopeEngine.Resources.shaders.basic_vertex.glsl"),
                FragmentShader.FromResources("HopeEngine.Resources.shaders.basic_fragment.glsl")
            );

            Hope.DefaultTexureShader = new ShaderProgram(
               VertexShader.FromResources("HopeEngine.Resources.shaders.textured_vertex.glsl"),
               FragmentShader.FromResources("HopeEngine.Resources.shaders.textured_fragment.glsl")
            );

            Hope.DefaultTextShader = new ShaderProgram(
               VertexShader.FromResources("HopeEngine.Resources.shaders.text_vertex.glsl"),
               FragmentShader.FromResources("HopeEngine.Resources.shaders.text_fragment.glsl")
            );

            Hope.DefaultTypeface = Android.Graphics.Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "minimal.otf");

            // The first scene setup is called once the engine is ready
            scene.Setup();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Use this to update game objects
            foreach (GameObject gameObject in scene.GameObjects)
            {
                gameObject.Update();
            }
        }

        // This gets called on each frame render
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            stopwatch.Start();
            Clear();

            foreach (GameObject gameObject in scene.GameObjects)
            {
                gameObject.Render();
            }

            SwapBuffers();

            // you only need to call this if you have delegates
            // registered that you want to have called
            base.OnRenderFrame(e);
            Hope.FrameTime = stopwatch.ElapsedMilliseconds / 1000.0f;
            stopwatch.Stop();
            stopwatch.Reset();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            // TODO try to get a full screen canvas instead
            int statusBarHeight = Resources.GetDimensionPixelSize(Resources.GetIdentifier("status_bar_height", "dimen", "android"));
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    // Finger down
                    scene.GameObjects.ForEach((o) => o.OnFingerDown(e.RawX, e.RawY - statusBarHeight));
                    break;
                case MotionEventActions.Up:
                    // Finger up
                    scene.GameObjects.ForEach((o) => o.OnFingerUp(e.RawX, e.RawY - statusBarHeight));
                    break;
                case MotionEventActions.Move:
                    // Finger move
                    scene.GameObjects.ForEach((o) => o.OnFingerMove(e.RawX, e.RawY - statusBarHeight));
                    break;
            }
            return true;
        }

        void CheckErrors()
        {
            ErrorCode errorCode = GL.GetErrorCode();
            if (errorCode != ErrorCode.NoError)
            {
                throw new Exception(errorCode.ToString());
            }
        }

    }
}
