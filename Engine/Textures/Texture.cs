using Android.Graphics;
using Android.Opengl;
using HopeEngine.Engine.Utils;
using OpenTK.Graphics.ES30;


namespace HopeEngine.Engine.Textures
{
    public class Texture
    {

        public float Width { get; private set; }
        public float Height { get; private set; }
        public int TextureCode { get; private set; }

        public Texture(Bitmap bitmap)
        {
            bitmap = ReverseBitmap(bitmap);
            Width = bitmap.Width;
            Height = bitmap.Height;
            GL.GenTextures(1, out int code);
            TextureCode = code;
            Bind();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            GLUtils.TexImage2D((int)TextureTarget.Texture2D, 0, bitmap, 0);
            GL.GenerateMipmap(TextureTarget.Texture2D);
        }

        public Texture Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, TextureCode);
            return this;
        }

        private static Bitmap ReverseBitmap(Bitmap src)
        {
            Android.Graphics.Matrix m = new Android.Graphics.Matrix();
            m.PreScale(1, -1);
            Bitmap dst = Bitmap.CreateBitmap(src, 0, 0, src.Width, src.Height, m, false);
            dst.Density = (int)Android.Util.DisplayMetricsDensity.Default;

            return dst;
        }

        public static Texture FromBytes(byte[] imageBytes)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            return new Texture(bitmap);
        }

        public static Texture FromResources(string resourceName, System.Type assemblyType = null)
        {
            byte[] imageBytes = FileUtils.ReadResourceBytes(resourceName, assemblyType);
            return FromBytes(imageBytes);
        }

    }
}