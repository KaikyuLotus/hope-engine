using System.Collections.Generic;
using HopeEngine.Engine.Textures;

namespace HopeEngine.Engine.TextSystem
{
    public class HopeFont
    {

        public Dictionary<char, Glyph> Glyphs { get; private set; }
        public Texture Texture { get; private set; }
        public float LineHeight { get; private set; }

        public HopeFont(Dictionary<char, Glyph> glyphs, Texture texture, float lineHeight)
        {
            Glyphs = glyphs;
            Texture = texture;
            LineHeight = lineHeight;
        }
    }
}