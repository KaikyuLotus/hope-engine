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
            Chararcter = chararcter;
            X = x;
            Y = y;
            Width = width;
            Height = height;
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