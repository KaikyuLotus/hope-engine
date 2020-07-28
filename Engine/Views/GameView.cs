using Android.Util;
using OpenTK;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Views
{
    public class GameView
    {

        private Matrix4 viewMatrix;
        private Matrix4 projectionMatrix;

        private int width;
        private int height;

        public Matrix4 ViewProjectionMatrix
        {
            get
            {
                return viewMatrix * projectionMatrix;
            }
        }

        public GameView(int width, int height, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            this.width = width;
            this.height = height;
            this.viewMatrix = viewMatrix;
            this.projectionMatrix = projectionMatrix;

            Log.Info("Hope", $"Setting Viewport to 0, 0, {width}, {height}");
            GL.Viewport(0, 0, width, height);
        }

        public static GameView DefaultView(int width, int height)
        {
            Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 1.0f, 1000.0f);
            Matrix4 viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 1.0f), Vector3.Zero, Vector3.UnitY);
            return new GameView(width, height, viewMatrix, projectionMatrix);
        }

    }
}