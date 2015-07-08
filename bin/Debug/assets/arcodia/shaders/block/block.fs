#version 330 core

in VertexData
{
	vec3 pos;
	vec4 color;
	vec2 coords;
} data;

out vec4 color;

uniform sampler2D tex;

void main()
{
	color = data.color;
}