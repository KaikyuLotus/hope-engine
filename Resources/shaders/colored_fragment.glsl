#version 300 es

#ifdef GL_ES
precision highp float;
#endif

uniform vec4 Color;
uniform vec4 BorderColor;
uniform vec4 BorderWidth;
uniform float AspectRatio;

in vec4 Position;

out vec4 FragColor;


void main()
{
   float maxX = 1.0 - BorderWidth.z;
   float minX = BorderWidth.x;
   float maxY = 1.0 - BorderWidth.w / AspectRatio;
   float minY = BorderWidth.y / AspectRatio;

   if (Position.x < maxX && Position.x > minX &&
       Position.y < maxY && Position.y > minY) {
     FragColor = Color;
   } else {
     FragColor = BorderColor;
   }
}