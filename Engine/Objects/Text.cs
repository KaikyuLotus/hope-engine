using System;
using System.Collections.Generic;
using Android.Graphics;
using HopeEngine.Engine.TextSystem;
using OpenTK.Graphics.ES30;

namespace HopeEngine.Engine.Objects
{
    public class Text : GameObject
    {

        private HopeFont hopeFont;

        private float[] uvs;
        private float[] indices;

        private string text;

        private OpenTK.Vector3 colorVector;

        public Text(string text, Color? color = null, Typeface typeface = null)
        {
            hopeFont = TextRenderer.CreateHopeFont(typeface ?? Hope.DefaultTypeface);
            Color tmpColor = color ?? Color.White;
            SetColor(tmpColor);
            SetText(text);
            
        }

        public void SetColor(Color color)
        {
            colorVector = new OpenTK.Vector3(color.R, color.G, color.B);
        }

        public void SetText(string text)
        {
            this.text = text;
            OnTextChange();
        }

        private void OnTextChange()
        {
            var uvsList = new List<float>();
            var indicesList = new List<float>();

            float lastX = 0;
            float lastY = 0;

            foreach (char c in text.ToCharArray())
            {
                if (c == '\n')
                {
                    lastX = 0;
                    lastY += hopeFont.LineHeight;
                    continue;
                }
                Glyph glyph = hopeFont.Glyphs.GetValueOrDefault(c, hopeFont.Glyphs['?']);

                float[] charUvs = glyph.ToFloatArray();

                uvsList.AddRange(charUvs);

                indicesList.AddRange(new float[]{
                    lastX,               lastY,
                    glyph.Width + lastX, lastY,
                    lastX,               glyph.Height + lastY,
                    lastX,               glyph.Height + lastY,
                    glyph.Width + lastX, glyph.Height + lastY,
                    glyph.Width + lastX, lastY
                });
                lastX += glyph.Width + 0.01f;

            }
            uvs = uvsList.ToArray();
            indices = indicesList.ToArray();
        }

        public override void Render()
        {

            Hope.DefaultTextShader.Use();

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            unsafe
            {
                fixed (float* pindices = indices)
                {
                    GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(pindices));
                };
                fixed (float* puvs = uvs)
                {
                    GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(puvs));
                };
            };

            Hope.DefaultTextShader
                .GetUniformLocation("mvpMatrix")
                .UniformMatrix4(Transform.CalculateModelMatrix() * Hope.DefaultView.ViewProjectionMatrix);

            Hope.DefaultTextShader
                .GetUniformLocation("ActualColor")
                .UniformVector3(colorVector);

            hopeFont.Texture.Bind();

            GL.DrawArrays(BeginMode.Triangles, 0, uvs.Length);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

        }

    }
}