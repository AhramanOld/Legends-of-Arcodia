using Arcodia.Renderer.Shaders;
using Arcodia.Worlds;
using Arcodia.Worlds.Chunks;
using OpenTK;
using System;
using System.Collections.Generic;

namespace Arcodia.Renderer.Worlds
{
    public class WorldRenderer : IDisposable
    {
        private Arcodia Arcodia;
        private World WorldObj;
        private RenderGlobal RenderGlobal;

        private VertexBuffer Buffer;

        private Dictionary<ChunkPos, ChunkRenderer> ChunkRenderers = new Dictionary<ChunkPos, ChunkRenderer>();

        public WorldRenderer(Arcodia game) : this(game, game.TheWorld) {}

        public WorldRenderer(Arcodia game, World world) : this(game, world, game.RenderGlobal) { }

        public WorldRenderer(Arcodia game, World world, RenderGlobal renderer)
        {
            this.Arcodia = game;
            this.WorldObj = world;
            this.RenderGlobal = renderer;
        }

        public void Init()
        {
            this.Buffer = new VertexBuffer();

            var pos = new ChunkPos(0, 0, 0);
            var chunk = new Chunk(this.WorldObj, pos);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        chunk.SetBlock(BlockList.Stone, i, j, k);
                    }
                }
            }

            this.ChunkRenderers.Add(pos, new ChunkRenderer(chunk, this.WorldObj));

            this.Buffer.Upload();
        }

        public void Update()
        {
            this.RenderGlobal.BlocksShader.Bind();

            var pos = this.Arcodia.ThePlayer.GetRenderPos(this.Arcodia.Timer.RenderTime);

            this.RenderGlobal.BlocksShader.SetUniform("model", Matrix4.Identity);
            this.RenderGlobal.BlocksShader.SetUniform("view", Matrix4.LookAt(pos, pos + this.Arcodia.ThePlayer.GetRotation(), new Vector3(0.0F, 1.0F, 0.0F)));
            this.RenderGlobal.BlocksShader.SetUniform("projection", RenderGlobal.ProjectionMatrix);

            foreach (var pair in this.ChunkRenderers)
            {
                pair.Value.Render();
            }

            this.Buffer.Draw();
            this.RenderGlobal.BlocksShader.UnBind();
        }

        public void Dispose()
        {
        }
    }
}