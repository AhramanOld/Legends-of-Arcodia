#version 330 core

layout (location = 0) in vec3 pos;
layout (location = 1) in vec4 color;
layout (location = 2) in vec2 coords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out VertexData
{
	vec3 pos;
	vec4 color;
	vec2 coords;
} data;

void main()
{
	data.pos = pos;
	data.color = color;
	data.coords = coords;
	gl_Position = projection * view * model * vec4(pos, 1.0);
}