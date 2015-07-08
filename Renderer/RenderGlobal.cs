using Arcodia.Renderer.Shaders;
using Arcodia.Renderer.Worlds;
using Arcodia.Worlds;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Arcodia.Renderer
{
    public class RenderGlobal : IDisposable
    {
        private Arcodia Arcodia;

        private WorldRenderer WorldRenderer;

        public float FieldOfView
        {
            get
            {
                return MathHelper.PiOver4;
            }
        }

        public float AspectRatio
        {
            get
            {
                return (float)this.Arcodia.Window.Width / (float)this.Arcodia.Window.Height;
            }
        }

        public Matrix4 ProjectionMatrix;

        public readonly Shader BlocksShader;
        public readonly Shader LightingShader;

        public RenderGlobal(Arcodia game)
        {
            this.Arcodia = game;

            this.BlocksShader = new Shader("Blocks", "block/block");
            this.LightingShader = new Shader("Lighting", "lighting/lighting");
        }

        public void SetWorld(World world)
        {
            if (world != null)
            {
                this.WorldRenderer = new WorldRenderer(this.Arcodia, world);

                this.WorldRenderer.Init();
            }
            else
            {
                if (this.WorldRenderer != null)
                {
                    this.WorldRenderer.Dispose();

                    this.WorldRenderer = null;
                }
            }
        }

        public void Init()
        {
            GL.ClearDepth(1.0D);
            GL.ClearColor(0.0F, 0.0F, 0.0F, 1.0F);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            this.BlocksShader.Init();
            this.LightingShader.Init();
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (this.WorldRenderer != null)
            {
                this.WorldRenderer.Update();
            }
        }

        public void Resize()
        {
            GL.Viewport(0, 0, this.Arcodia.Window.Width, this.Arcodia.Window.Height);

            this.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(this.FieldOfView, this.AspectRatio, 0.05F, 1024.0F);
        }

        public void Dispose()
        {
            this.SetWorld(null);
        }
    }
}