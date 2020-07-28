using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace HopeEngine.Engine.TextSystem
{
    public class Glyph
    {

        public char Chararcter { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        public Glyph(char chararcter, float x, float y, float width, float height)
        {
            this.Chararcter = chararcter;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public float[] ToFloatArray()
        {
            return new float[] {
                X,         Height + Y,
                Width + X, Height + Y,
                X,         Y,
                X,         Y,
                Width + X, Y,
                Width + X, Height + Y
            };
        }

    }
}