using Android.Content;
using Android.Util;
using Android.Views;
using HopeEngine.Engine.Objects;
using HopeEngine.Engine.Scenes;
using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.Shaders.Internal;
using HopeEngine.Engine.Views;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Platform.Android;
using System;

namespace HopeEngine.Engine
{

    public class HopeEngineView : AndroidGameView
    {

        public Action OnReady;

        public Scene Scene { get; private set; }

        public HopeEngineView(Context context, Scene scene) : base(context)
        {
            Scene = scene;
        }

        public void ChangeScene(Scene scene)
        {
            // TODO do something like transitions ?
            Scene = scene;
            Scene.Setup();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Run(240);
        }

        private void LoadStandardSettings()
        {
            try
            {
                RenderOnUIThread = true;
                // the default GraphicsMode that is set consists of (16, 16, 0, 0, 2, false)
                Log.Debug("Hope", "Loading with default settings");
                // GraphicsMode = new GraphicsMode(new ColorFormat(32), 24, 8, 4,);
                GraphicsMode = new GraphicsMode(32, 24, 8, 4, 0, 1, true);
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
            ContextRenderingApi = GLVersion.ES3;
            LoadStandardSettings();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Width = Resources.DisplayMetrics.WidthPixels;
            Height = Resources.DisplayMetrics.HeightPixels;
            Hope.ScreenSize = new Vector2(Width, Height);

            Hope.DefaultView = GameView.DefaultView(Width, Height);

            // Will be removed
            Log.Info("Hope", "Creating Shader programs");
            HopeShaders.DefaultShader = new ShaderProgram(
                VertexShader.FromResources("HopeEngine.Resources.shaders.basic_vertex.glsl"),
                FragmentShader.FromResources("HopeEngine.Resources.shaders.basic_fragment.glsl")
            );

            HopeShaders.DefaultTexureShader = new ShaderProgram(
               VertexShader.FromResources("HopeEngine.Resources.shaders.textured_vertex.glsl"),
               FragmentShader.FromResources("HopeEngine.Resources.shaders.textured_fragment.glsl")
            );

            HopeShaders.DefaultColorShader = new ShaderProgram(
               VertexShader.FromResources("HopeEngine.Resources.shaders.colored_vertex.glsl"),
               FragmentShader.FromResources("HopeEngine.Resources.shaders.colored_fragment.glsl")
            );

            HopeShaders.DefaultTextShader = new TextShader();

            Hope.DefaultTypeface = Android.Graphics.Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "minimal.otf");

            Hope.EngineView = this;

            if (OnReady != null)
            {
                OnReady.Invoke();
            }
            // The first scene setup is called once the engine is ready
            Scene.Setup();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Hope.FrameTime = e.Time;
            // Use this to update game objects
            foreach (GameObject gameObject in Scene.GameObjects)
            {
                gameObject.InternalUpdate();
            }
        }

        // This gets called on each frame render
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            Clear();

            foreach (GameObject gameObject in Scene.GameObjects)
            {
                gameObject.InternalRender();
            }

            SwapBuffers();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    // Finger down
                    foreach (GameObject gameObject in Scene.GameObjects)
                    {
                        gameObject.InternalOnFingerDown(e.RawX, e.RawY);
                    }
                    break;
                case MotionEventActions.Up:
                    // Finger up
                    foreach (GameObject gameObject in Scene.GameObjects)
                    {
                        gameObject.InternalOnFingerUp(e.RawX, e.RawY);
                    }
                    break;
                case MotionEventActions.Move:
                    // Finger move
                    foreach (GameObject gameObject in Scene.GameObjects)
                    {
                        gameObject.InternalOnFingerMove(e.RawX, e.RawY);
                    }
                    break;
            }
            return true;
        }

    }
}
