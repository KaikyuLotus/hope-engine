#version 300 es

layout (location = 0) in vec3 aPos;

out vec4 Position;

uniform mat4 mvpMatrix;

void main()
{
    gl_Position = mvpMatrix * vec4(aPos, 1.0);
    Position = vec4(aPos, 1.0);
}