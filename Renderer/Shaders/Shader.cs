using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Arcodia.Renderer.Shaders
{
	public class Shader : IDisposable
	{
		private string ShaderName;
		private string ShaderFile;

		private string VertexShaderProgram;
		private string FragmentShaderProgram;
		private string GeometryShaderProgram;
		private string TessControlShaderProgram;
		private string TessEvaluationShaderProgram;
		private string ComputeShaderProgram;

		private bool HasVertexShader;
		private bool HasFragmentShader;
		private bool HasGeometryShader;
		private bool HasTessControlShader;
		private bool HasTessEvaluationShader;
		private bool HasComputeShader;

		private int VertexShaderId = -1;
		private int FragmentShaderId = -1;
		private int GeometryShaderId = -1;
		private int TessControlShaderId = -1;
		private int TessEvaluationShaderId = -1;
		private int ComputeShaderId = -1;

		private int ProgramId = -1;

		private Dictionary<string, int> Uniforms = new Dictionary<string, int>();

		public Shader(string name, string file, bool vertex, bool fragment, bool geometry, bool control, bool evaluation, bool compute)
		{
			this.ShaderName = name;
			this.ShaderFile = file;
			this.HasVertexShader = vertex;
			this.HasFragmentShader = fragment;
			this.HasGeometryShader = geometry;
			this.HasTessControlShader = control;
			this.HasTessEvaluationShader = evaluation;
			this.HasComputeShader = compute;
		}

		public Shader(string name, string file, bool vertex, bool fragment, bool geometry, bool compute) : this(name, file, vertex, fragment, geometry, false, false, compute) {}

		public Shader(string name, string file, bool vertex, bool fragment, bool geometry) : this(name, file, vertex, fragment, geometry, false) {}

		public Shader(string name, string file) : this(name, file, true, true, false) {}

		public Shader(string name, bool vertex, bool fragment, bool geometry, bool compute) : this(name, name, vertex, fragment, geometry, false, false, compute) {}

		public Shader(string name, bool vertex, bool fragment, bool geometry) : this(name, name, vertex, fragment, geometry, false) {}

		public Shader(string name) : this(name, name) {}

		public Shader(string name, string file, ShaderType type)
		{
			this.ShaderName = name;
			this.ShaderFile = file;

			switch (type)
			{
			case ShaderType.VertexShader:
				this.HasVertexShader = true;
				break;
			case ShaderType.FragmentShader:
				this.HasFragmentShader = true;
				break;
			case ShaderType.GeometryShader:
				this.HasGeometryShader = true;
				break;
			case ShaderType.TessControlShader:
				this.HasTessControlShader = true;
				break;
			case ShaderType.TessEvaluationShader:
				this.HasTessEvaluationShader = true;
				break;
			case ShaderType.ComputeShader:
				this.HasComputeShader = true;
				break;
			}
		}

		public Shader(string name, ShaderType type) : this(name, name, type) {}

		private void CheckError()
		{
			var error = GL.GetError();

			if (error != ErrorCode.NoError)
			{
				Console.WriteLine(String.Format("OpenGL has ran to an error: {0}", error));

				string info;

				if (this.ProgramId != -1)
				{
					info = GL.GetProgramInfoLog(this.ProgramId);
					Console.WriteLine(String.Format("Additional Program Information:\nProgram ID: {0}\nProgram Error Info: {1}", this.ProgramId, info));
				}

				throw new SystemException();
			}
		}

		private void LoadShaderPrograms()
		{
			if (this.HasVertexShader)
			{
                this.VertexShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".vs");
			}

			if (this.HasFragmentShader)
			{
                this.FragmentShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".fs");
			}

			if (this.HasGeometryShader)
			{
                this.GeometryShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".gs");
			}

			if (this.HasTessControlShader)
			{
                this.TessControlShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".tcs");
			}

			if (this.HasTessEvaluationShader)
			{
                this.TessEvaluationShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".tes");
			}

			if (this.HasComputeShader)
			{
				this.ComputeShaderProgram = File.ReadAllText("assets/arcodia/shaders/" + this.ShaderFile + ".cs");
			}
		}

		private void CreateProgram()
		{
			if (this.ProgramId != -1)
			{
				GL.DeleteProgram(this.ProgramId);
			}

			this.ProgramId = GL.CreateProgram();
		}

		private void Compile()
		{
			this.CreateProgram();

			int error;

			if (this.HasVertexShader && this.VertexShaderProgram != "")
			{
				this.VertexShaderId = GL.CreateShader(ShaderType.VertexShader);
				GL.ShaderSource(this.VertexShaderId, this.VertexShaderProgram);
				GL.CompileShader(this.VertexShaderId);

				GL.GetShader(this.VertexShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling vertex shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.VertexShaderId));

					GL.DeleteShader(this.VertexShaderId);

					throw new SystemException();
				}

				GL.AttachShader(this.ProgramId, this.VertexShaderId);
			}

			if (this.HasFragmentShader && this.FragmentShaderProgram != "")
			{
				this.FragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(this.FragmentShaderId, this.FragmentShaderProgram);
				GL.CompileShader(this.FragmentShaderId);

				GL.GetShader(this.FragmentShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling fragment shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.FragmentShaderId));

					GL.DeleteShader(this.FragmentShaderId);

					throw new SystemException();
				}

				GL.AttachShader(this.ProgramId, this.FragmentShaderId);
			}

			if (this.HasGeometryShader && this.GeometryShaderProgram != "")
			{
				this.GeometryShaderId = GL.CreateShader(ShaderType.GeometryShader);
				GL.ShaderSource(this.GeometryShaderId, this.GeometryShaderProgram);
				GL.CompileShader(this.GeometryShaderId);

				GL.GetShader(this.GeometryShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling geometry shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.GeometryShaderId));

					GL.DeleteShader(this.GeometryShaderId);

					throw new SystemException();
				}

				this.CheckError();

				GL.AttachShader(this.ProgramId, this.GeometryShaderId);
			}

			if (this.HasTessControlShader && this.TessControlShaderProgram != "")
			{
				this.TessControlShaderId = GL.CreateShader(ShaderType.TessControlShader);
				GL.ShaderSource(this.TessControlShaderId, this.TessControlShaderProgram);
				GL.CompileShader(this.TessControlShaderId);

				GL.GetShader(this.TessControlShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling tessellation control shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.TessControlShaderId));

					GL.DeleteShader(this.TessControlShaderId);

					throw new SystemException();
				}

				this.CheckError();

				GL.AttachShader(this.ProgramId, this.TessControlShaderId);
			}

			if (this.HasTessEvaluationShader && this.TessEvaluationShaderProgram != "")
			{
				this.TessEvaluationShaderId = GL.CreateShader(ShaderType.TessEvaluationShader);
				GL.ShaderSource(this.TessEvaluationShaderId, this.TessEvaluationShaderProgram);
				GL.CompileShader(this.TessEvaluationShaderId);

				GL.GetShader(this.TessEvaluationShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling tessellation evaluation shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.TessEvaluationShaderId));

					GL.DeleteShader(this.TessEvaluationShaderId);

					throw new SystemException();
				}

				this.CheckError();

				GL.AttachShader(this.ProgramId, this.TessEvaluationShaderId);
			}

			if (this.HasComputeShader && this.ComputeShaderProgram != "")
			{
				this.ComputeShaderId = GL.CreateShader(ShaderType.ComputeShader);
				GL.ShaderSource(this.ComputeShaderId, this.ComputeShaderProgram);
				GL.CompileShader(this.ComputeShaderId);

				GL.GetShader(this.ComputeShaderId, ShaderParameter.CompileStatus, out error);

				if (error == 0)
				{
					Console.WriteLine("An error occurred while compiling compute shader. Addition Info: ");
					Console.WriteLine(GL.GetShaderInfoLog(this.ComputeShaderId));

					GL.DeleteShader(this.ComputeShaderId);

					throw new SystemException();
				}

				this.CheckError();

				GL.AttachShader(this.ProgramId, this.ComputeShaderId);
			}

			GL.LinkProgram(this.ProgramId);

			GL.ValidateProgram(this.ProgramId);

			GL.GetProgram(this.ProgramId, GetProgramParameterName.LinkStatus, out error);

			if (error == 0)
			{
				Console.WriteLine("An error occurred while linking program. Addition Info: ");
				var info = GL.GetProgramInfoLog(this.ProgramId);
				Console.WriteLine(String.Format("Program ID: {0}\nProgram Error Info: {1}", this.ProgramId, info));

				throw new SystemException();
			}

			this.CheckError();
		}

		public void Init()
		{
			this.LoadShaderPrograms();
			this.Compile();
		}

		public void Bind()
		{
			GL.UseProgram(this.ProgramId);
		}

        internal void UnBind()
        {
            GL.UseProgram(0);
        }

		public void SetUniform(string name, int x)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform1(this.Uniforms[name], x);
				this.CheckError();
			}
		}

		public void SetUniform(string name, uint x)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform1(this.Uniforms[name], x);
				this.CheckError();
			}
		}

		public void SetUniform(string name, float x)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform1(this.Uniforms[name], x);
				this.CheckError();
			}
		}

		public void SetUniform(string name, double x)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform1(this.Uniforms[name], x);
				this.CheckError();
			}
		}

		public void SetUniform(string name, int x, int y)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform2(this.Uniforms[name], x, y);
				this.CheckError();
			}
		}

		public void SetUniform(string name, uint x, uint y)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform2(this.Uniforms[name], x, y);
				this.CheckError();
			}
		}

		public void SetUniform(string name, float x, float y)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform2(this.Uniforms[name], x, y);
				this.CheckError();
			}
		}

		public void SetUniform(string name, double x, double y)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform2(this.Uniforms[name], x, y);
				this.CheckError();
			}
		}

		public void SetUniform(string name, int x, int y, int z)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform3(this.Uniforms[name], x, y, z);
				this.CheckError();
			}
		}

		public void SetUniform(string name, uint x, uint y, uint z)
		{
			if (this.ProgramId != -1)
			{
				GL.Uniform3(this.Uniforms[name], x, y, z);
				this.CheckError();
			}
		}

		public void SetUniform(string name, float x, float y, float z)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform3(this.Uniforms[name], x, y, z);
				this.CheckError();
			}
		}

		public void SetUniform(string name, double x, double y, double z)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform3(this.Uniforms[name], x, y, z);
				this.CheckError();
			}
		}

		public void SetUniform(string name, int x, int y, int z, int w)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform4(this.Uniforms[name], x, y, z, w);
				this.CheckError();
			}
		}

		public void SetUniform(string name, uint x, uint y, uint z, uint w)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform4(this.Uniforms[name], x, y, z, w);
				this.CheckError();
			}
		}

		public void SetUniform(string name, float x, float y, float z, float w)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform4(this.Uniforms[name], x, y, z, w);
				this.CheckError();
			}
		}

		public void SetUniform(string name, double x, double y, double z, double w)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.Uniform4(this.Uniforms[name], x, y, z, w);
				this.CheckError();
			}
		}

		public void SetUniform(string name, Vector2 vec)
		{
			this.SetUniform(name, vec.X, vec.Y);
		}

		public void SetUniform(string name, Vector2d vec)
		{
			this.SetUniform(name, vec.X, vec.Y);
		}

		public void SetUniform(string name, Vector3 vec)
		{
			this.SetUniform(name, vec.X, vec.Y, vec.Z);
		}

		public void SetUniform(string name, Vector3d vec)
		{
			this.SetUniform(name, vec.X, vec.Y, vec.Z);
		}

		public void SetUniform(string name, Vector4 vec)
		{
			this.SetUniform(name, vec.X, vec.Y, vec.Z, vec.W);
		}

		public void SetUniform(string name, Vector4d vec)
		{
			this.SetUniform(name, vec.X, vec.Y, vec.Z, vec.W);
		}

		public void SetUniform(string name, Color color)
		{
			this.SetUniform(name, color.R / 255.0F, color.G / 255.0F, color.B / 255.0F, color.A / 255.0F);
		}

		public void SetUniform(string name, Matrix2 mat)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.UniformMatrix2(this.Uniforms[name], false, ref mat);
				this.CheckError();
			}
		}

		public void SetUniform(string name, Matrix3 mat)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.UniformMatrix3(this.Uniforms[name], false, ref mat);
				this.CheckError();
			}
		}

		public void SetUniform(string name, Matrix4 mat)
		{
			if (this.ProgramId != -1)
			{
				if (!this.Uniforms.ContainsKey(name))
				{
					this.Uniforms.Add(name, GL.GetUniformLocation(this.ProgramId, name));
				}

				GL.UniformMatrix4(this.Uniforms[name], false, ref mat);
			}
		}

        public int GetProgram()
        {
            return this.ProgramId;
        }

		public void Dispose()
		{
			if (this.HasVertexShader)
			{
				GL.DeleteShader(this.VertexShaderId);
			}

			if (this.HasFragmentShader)
			{
				GL.DeleteShader(this.FragmentShaderId);
			}

			if (this.HasGeometryShader)
			{
				GL.DeleteShader(this.GeometryShaderId);
			}

			if (this.HasTessControlShader)
			{
				GL.DeleteShader(this.TessControlShaderId);
			}

			if (this.HasTessEvaluationShader)
			{
				GL.DeleteShader(this.TessEvaluationShaderId);
			}

			if (this.HasComputeShader)
			{
				GL.DeleteShader(this.ComputeShaderId);
			}

			GL.DeleteProgram(this.ProgramId);
		}
    }
}