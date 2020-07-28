#version 300 es

#ifdef GL_ES
precision highp float;
#endif

out vec4 fragColor;

void main()
{
    fragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}