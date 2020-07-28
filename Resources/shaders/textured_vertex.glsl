#version 300 es

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 mvpMatrix;

void main()
{
    gl_Position = mvpMatrix * vec4(aPos, 1.0);
    TexCoord = aTexCoord;
}