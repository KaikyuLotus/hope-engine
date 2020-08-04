using Android.Graphics;
using HopeEngine.Engine.Shaders;
using OpenTK;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Objects.Geometrics
{
    public class ColoredSquare : BaseRenderable
    {

        public Color BackgroundColor;

        public Color BorderColor = Color.Transparent;

        public Vector4 BorderWidth = Vector4.Zero;

        public ColoredSquare(Color color) : base(HopeShaders.DefaultColorShader, Square.Vertices)
        {
            BackgroundColor = color;
        }

        public override void Render()
        {
            shader.Use();
            shader.GetUniformLocation("mvpMatrix").UniformMatrix4(Transform.CalculateModelMatrix() * View.ViewProjectionMatrix);
            shader.GetUniformLocation("Color").UniformColor(BackgroundColor);
            shader.GetUniformLocation("BorderColor").UniformColor(BorderColor);
            shader.GetUniformLocation("BorderWidth").UniformVector4(BorderWidth);
            shader.GetUniformLocation("AspectRatio").UniformFloat(Transform.Scale.Y / Transform.Scale.X);

            GL.BindVertexArray(VAO);
            GL.DrawArrays(beginMode, 0, vertices.Length / groupSize);
            GL.BindVertexArray(0);
        }

    }
}