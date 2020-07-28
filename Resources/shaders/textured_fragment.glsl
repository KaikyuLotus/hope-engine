#version 300 es

#ifdef GL_ES
precision highp float;
#endif

out vec4 FragColor;

in vec2 TexCoord;

uniform sampler2D BaseTexture;

void main()
{
    FragColor = texture(BaseTexture, TexCoord);
}