using Android.Util;
using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.Textures;
using OpenTK.Graphics.ES30;
using System;

namespace HopeEngine.Engine.Objects
{
    public class TexturedRenderable : BaseRenderable
    {

        protected float[] uvs;
        protected int uvGroupSize;
        protected Texture texture;

        protected int VBOv;
        protected int VBOu;

        public TexturedRenderable(
            ShaderProgram shader, Texture texture,
            float[] vertices, float[] uvs,
            BeginMode beginMode = BeginMode.Triangles,
            int uvGroupSize = 2, int groupSize = 3,
            VertexAttribPointerType pointerType = VertexAttribPointerType.Float) : base(shader, vertices, beginMode, groupSize, pointerType)
        {
            this.uvs = uvs;
            this.uvGroupSize = uvGroupSize;
            this.texture = texture;

        }

        public override void Prepare()
        {
            Log.Debug("Hope.TexturedRenderable.Prepare", "Preparing buffers...");
            GL.GenVertexArrays(1, out VAO);
            GL.GenBuffers(1, out VBOv);
            GL.GenBuffers(1, out VBOu);
            UpdateVAO();
        }

        public override void UpdateVAO()
        {

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOv);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * vertices.Length), vertices, BufferUsage.StaticDraw);
            GL.VertexAttribPointer(0, groupSize, VertexAttribPointerType.Float, false, groupSize * sizeof(float), (IntPtr)0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOu);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * uvs.Length), uvs, BufferUsage.StaticDraw);
            GL.VertexAttribPointer(1, uvGroupSize, VertexAttribPointerType.Float, false, uvGroupSize * sizeof(float), (IntPtr)0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public override void Render()
        {
            shader.Use();
            shader.GetUniformLocation("mvpMatrix").UniformMatrix4(Transform.CalculateModelMatrix() * View.ViewProjectionMatrix);
            texture.Bind();

            GL.BindVertexArray(VAO);
            GL.DrawArrays(beginMode, 0, vertices.Length / groupSize);
            GL.BindVertexArray(0);

        }
    }
}