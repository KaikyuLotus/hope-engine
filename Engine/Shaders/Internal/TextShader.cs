namespace HopeEngine.Engine.Shaders.Internal
{
    public class TextShader : ShaderProgram
    {

        public readonly UniformLocation MvpMatrixLocation;
        public readonly UniformLocation ActualColorLocation;

        public TextShader() : base(
            VertexShader.FromResources("HopeEngine.Resources.shaders.text_vertex.glsl"),
            FragmentShader.FromResources("HopeEngine.Resources.shaders.text_fragment.glsl"))
        {
            MvpMatrixLocation = GetUniformLocation("mvpMatrix");
            ActualColorLocation = GetUniformLocation("ActualColor");
        }

    }
}