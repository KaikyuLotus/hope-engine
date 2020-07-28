#version 300 es

layout (location = 0) in vec3 aPos;
out vec4 colour;

uniform mat4 mvpMatrix;

const vec4 white = vec4(1.0);

void main()
{
    colour = white;
    gl_Position = mvpMatrix * vec4(aPos.x, aPos.y, aPos.z, 1.0);
}