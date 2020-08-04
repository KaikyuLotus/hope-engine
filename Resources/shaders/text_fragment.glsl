#version 300 es

#ifdef GL_ES
precision highp float;
#endif

out vec4 FragColor;

in vec2 TexCoord;

uniform sampler2D BaseTexture;
uniform vec4 ActualColor;

void main()
{
    FragColor = vec4(ActualColor.r, ActualColor.g, ActualColor.b, texture(BaseTexture, TexCoord).a);
}