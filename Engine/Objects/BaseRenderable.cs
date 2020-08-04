using Android.Util;
using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.Views;
using OpenTK.Graphics.ES30;
using System;

namespace HopeEngine.Engine.Objects
{
    public class BaseRenderable : GameObject
    {

        protected float[] vertices;
        protected BeginMode beginMode;
        protected int groupSize;
        protected VertexAttribPointerType pointerType;
        protected ShaderProgram shader;

        public GameView View;

        protected int VAO;
        protected int VBO;

        public BaseRenderable(ShaderProgram shader, float[] vertices, BeginMode beginMode = BeginMode.Triangles, int groupSize = 3, VertexAttribPointerType pointerType = VertexAttribPointerType.Float)
        {
            this.vertices = vertices;
            this.beginMode = beginMode;
            this.groupSize = groupSize;
            this.pointerType = pointerType;
            this.shader = shader;
            View = Hope.DefaultView;
        }

        public override void Prepare()
        {
            Log.Debug("Hope.BaseRenderable.Prepare", "Preparing buffers...");
            GL.GenVertexArrays(1, out VAO);
            GL.GenBuffers(1, out VBO);
            UpdateVAO();
        }

        public virtual void UpdateVAO()
        {

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * vertices.Length), vertices, BufferUsage.StaticDraw);
            GL.VertexAttribPointer(0, groupSize, VertexAttribPointerType.Float, false, groupSize * sizeof(float), (IntPtr)0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public override void Render()
        {
            shader.Use();
            shader.GetUniformLocation("mvpMatrix").UniformMatrix4(Transform.CalculateModelMatrix() * View.ViewProjectionMatrix);

            GL.BindVertexArray(VAO);
            GL.DrawArrays(beginMode, 0, vertices.Length / groupSize);
            GL.BindVertexArray(0);
        }
    }
}