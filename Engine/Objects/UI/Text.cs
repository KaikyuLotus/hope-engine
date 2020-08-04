using Android.Graphics;
using HopeEngine.Engine.Shaders;
using HopeEngine.Engine.TextSystem;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HopeEngine.Engine.Objects.UI
{

    public enum TextHorizontalAlignment
    {
        LEFT,
        RIGHT,
        CENTER
    }

    public enum TextVerticalAlignment
    {
        TOP,
        BOTTOM,
        CENTER
    }

    public class Text : TexturedRenderable
    {

        private HopeFont hopeFont;

        public int FontSize = 16;

        private string text;

        public Color Color;

        public float Width = 0.0f;

        public float Height = 0.0f;


        private TextHorizontalAlignment HAlign = TextHorizontalAlignment.LEFT;

        private TextVerticalAlignment VAlign = TextVerticalAlignment.TOP;

        public Text(string text = "", Color? color = null, Typeface typeface = null) : base(null, null, null, null, groupSize: 2)
        {
            hopeFont = TextRenderer.CreateHopeFont(typeface ?? Hope.DefaultTypeface);
            Color = color ?? Color.White;
            SetText(text);
        }

        public void SetText(string text)
        {
            this.text = text;
            OnTextChange();
            base.UpdateVAO();
        }

        public void SetTextVerticalAlign(TextVerticalAlignment align)
        {
            VAlign = align;
            OnTextChange();
            base.UpdateVAO();
        }

        public void SetTextHorizontalAlign(TextHorizontalAlignment align)
        {
            HAlign = align;
            OnTextChange();
            base.UpdateVAO();
        }

        private void OnTextChange()
        {
            List<float> uvsList = new List<float>();
            List<float> verticesList = new List<float>();

            char[] charArray = text.ToCharArray();

            float lastX = 0;
            float lastY = 0;
            float totalHeight = hopeFont.LineHeight * FontSize * (charArray.Count((c) => c == '\n') + 1);
            float totalWidth = 0;

            float maxX = hopeFont.Glyphs.Max((g) => g.Value.X);
            float maxY = hopeFont.Glyphs.Max((g) => g.Value.Y);

            float diffX = 1.0f - maxX;
            float diffY = 1.0f - maxY;

            foreach (char c in HAlign == TextHorizontalAlignment.RIGHT ? charArray.Reverse() : charArray)
            {
                if (c == '\n')
                {
                    lastX = 0;
                    lastY += hopeFont.LineHeight * FontSize;
                    continue;
                }
                Glyph glyph = hopeFont.Glyphs.GetValueOrDefault(c, hopeFont.Glyphs['?']);

                float[] charUvs = glyph.ToFloatArray();

                uvsList.AddRange(charUvs);

                float xCoordinate = lastX + (glyph.Width + diffX) * FontSize;
                float yCoordinate = lastY + (glyph.Height + diffY) * FontSize;

                verticesList.AddRange(new float[]{
                    lastX,       lastY,
                    xCoordinate, lastY,
                    lastX,       yCoordinate,
                    lastX,       yCoordinate,
                    xCoordinate, yCoordinate,
                    xCoordinate, lastY
                });
                // TODO some character acts wierd with TextHorizontalAlignment.RIGHT
                lastX += ((glyph.Width + diffX + 0.01f) * FontSize) * (HAlign == TextHorizontalAlignment.RIGHT ? -1.0f : 1.0f);
                totalWidth += (glyph.Width + diffX + 0.01f) * FontSize;

            }
            uvs = uvsList.ToArray();
            vertices = verticesList.ToArray();

            float parentWidth = Parent == null ? Hope.ScreenSize.X : Parent.Transform.Scale.X;
            float parentHeight = Parent == null ? Hope.ScreenSize.Y : Parent.Transform.Scale.Y;

            Width = totalWidth * 10.0f * Transform.Scale.X;
            Height = totalHeight * 10.0f * Transform.Scale.Y;

            if (HAlign == TextHorizontalAlignment.CENTER)
            {
                Transform.Position.X = parentWidth / 2.0f - Width / 2.0f;
            }
            if (VAlign == TextVerticalAlignment.CENTER)
            {
                Transform.Position.Y = parentHeight / 2.0f - Height;  // TODO this is not correct, the text height should be divided by two
            }
            if (VAlign == TextVerticalAlignment.BOTTOM)
            {
                Transform.Position.Y = parentHeight - Height;
            }
        }

        public override void Prepare()
        {
            OnTextChange();
            base.Prepare();
        }

        public override void Render()
        {
            HopeShaders.DefaultTextShader.Use();
            HopeShaders.DefaultTextShader.MvpMatrixLocation.UniformMatrix4(Transform.CalculateModelMatrix() * Hope.DefaultView.ViewProjectionMatrix);
            HopeShaders.DefaultTextShader.ActualColorLocation.UniformColor(Color);

            hopeFont.Texture.Bind();

            GL.BindVertexArray(VAO);
            GL.DrawArrays(beginMode, 0, vertices.Length / groupSize);
            GL.BindVertexArray(0);
        }

    }
}