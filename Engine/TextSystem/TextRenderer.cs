using Android.Graphics;
using HopeEngine.Engine.Textures;
using System.Collections.Generic;
using static Android.Graphics.Paint;

namespace HopeEngine.Engine.TextSystem
{
    public static class TextRenderer
    {

        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890?!|òèàùçé§ ><_-.:,;ì^'\"£$%&/()=[]{}\\~";

        public static HopeFont CreateHopeFont(Typeface typeface)
        {
            int bitmapWH = 1024;
            
            int textSize = 160;

            // double scale = DeviceDisplay.MainDisplayInfo.Density;
            Paint paint = new Paint(PaintFlags.AntiAlias)
            {
                Color = Color.White,
                TextSize = textSize
            };

            paint.SetTypeface(typeface);
            paint.SetShadowLayer(0.2f, 0f, 1f, Color.DarkGray);

            // First create the canvas 1024x1024 with alpha
            Bitmap.Config bitmapConfig = Bitmap.Config.Argb8888;
            Bitmap bitmap = Bitmap.CreateBitmap(bitmapWH, bitmapWH, bitmapConfig);
            Canvas canvas = new Canvas(bitmap);
            // canvas.DrawColor(Color.DarkGray);

            Dictionary<char, Glyph> glyphs = new Dictionary<char, Glyph>();

            FontMetrics metrics = paint.GetFontMetrics();
            float lineHeight = metrics.Bottom - metrics.Top;
            float ascent = metrics.Ascent;

            Android.Util.Log.Debug("HopeA", "Ascent:      " + ascent);
            Android.Util.Log.Debug("HopeA", "Line Height: " + lineHeight);

            List<char> missingChars = new List<char>();
            missingChars.AddRange(chars.ToCharArray());

            int currentLineIndex = 1;

            while (missingChars.Count > 0)
            {
                float currentLineHeight = lineHeight * currentLineIndex;
                int currentCharsCount;
                int index = -1;
                string currentChars;
                do
                {
                    index++;
                    currentCharsCount = missingChars.Count - index;
                    currentChars = string.Join("", missingChars.GetRange(0, currentCharsCount));
                } while (paint.MeasureText(currentChars) > bitmapWH);


                // Now we have the index of chars to write on this line

                missingChars.RemoveRange(0, currentCharsCount);
                canvas.DrawText(currentChars, 0, currentLineHeight, paint);

                float lastX = 0;
                foreach (char c in currentChars.ToCharArray())
                {
                    var widths = new float[1];
                    paint.GetTextWidths(c.ToString(), widths);
                    var width = widths[0];

                    float relativeAscent = lineHeight + ascent + 5;
                    Android.Util.Log.Debug("HopeA", "Relative ascent: " + relativeAscent);

                    float uvX = lastX / bitmapWH;
                    float uvY = (currentLineHeight + relativeAscent) / bitmapWH;
                    float uvW = width / bitmapWH;
                    float uvH = lineHeight / bitmapWH;

                    Glyph glyph = new Glyph(c, uvX, 1.0f - uvY, uvW, uvH);

                    glyphs.Add(c, glyph);
                    lastX += width;
                }

                currentLineIndex++;
            }

            return new HopeFont(glyphs, new Texture(bitmap), lineHeight / bitmapWH);

        }

    }
}