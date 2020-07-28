using System;
using HopeEngine.Engine.Textures;
using HopeEngine.Engine.Shaders;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Objects
{
    public class TexturedRenderable : BaseRenderable
    {

        private readonly float[] uvs;
        private readonly int uvGroupSize;
        private readonly Texture texture;

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

        
        public override void Render()
        {
            shader.Use();
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            unsafe
            {
                fixed (float* pvertices = vertices)
                {
                    GL.VertexAttribPointer(0, groupSize, VertexAttribPointerType.Float, false, 0, new IntPtr(pvertices));
                };
                fixed (float* puvs = uvs)
                {
                    GL.VertexAttribPointer(1, uvGroupSize, VertexAttribPointerType.Float, false, 0, new IntPtr(puvs));
                };
            };
            shader.GetUniformLocation("mvpMatrix").UniformMatrix4(Transform.CalculateModelMatrix() * View.ViewProjectionMatrix);
            texture.Bind();

            GL.DrawArrays(this.beginMode, 0, vertices.Length);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

        }
    }
}