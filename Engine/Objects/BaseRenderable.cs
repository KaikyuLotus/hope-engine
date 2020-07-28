using System;
using HopeEngine.Engine.Views;
using HopeEngine.Engine.Shaders;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Objects
{
    public class BaseRenderable : GameObject
    {

        protected readonly float[] vertices;
        protected readonly BeginMode beginMode;
        protected readonly int groupSize;
        protected readonly VertexAttribPointerType pointerType;
        protected readonly ShaderProgram shader;

        public GameView View;

        public BaseRenderable(ShaderProgram shader, float[] vertices, BeginMode beginMode = BeginMode.Triangles, int groupSize = 3, VertexAttribPointerType pointerType = VertexAttribPointerType.Float)
        {
            this.vertices = vertices;
            this.beginMode = beginMode;
            this.groupSize = groupSize;
            this.pointerType = pointerType;
            this.shader = shader;
            View = Hope.DefaultView;
        }

        override
        public void Render()
        {
            shader.Use();
            GL.EnableVertexAttribArray(0);
            unsafe
            {
                fixed (float* pvertices = vertices)
                {
                    GL.VertexAttribPointer(0, groupSize, VertexAttribPointerType.Float, false, 0, new IntPtr(pvertices));
                };
            };

            shader.GetUniformLocation("mvpMatrix").UniformMatrix4(Transform.CalculateModelMatrix() * View.ViewProjectionMatrix);
            GL.DrawArrays(this.beginMode, 0, vertices.Length);
            GL.DisableVertexAttribArray(0);
        }
    }
}