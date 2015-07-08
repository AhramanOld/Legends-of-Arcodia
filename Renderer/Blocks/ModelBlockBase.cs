using Arcodia.Blocks;
using Arcodia.Worlds;

namespace Arcodia.Renderer.Blocks
{
    public abstract class ModelBlockBase : IBlockModel
    {
        protected bool ShouldRenderAllFaces;

        protected World WorldObj;

        public void SetWorld(World world)
        {
            this.WorldObj = world;
        }

        public void SetRenderAllFaces(bool value)
        {
            this.ShouldRenderAllFaces = value;
        }

        public abstract bool RenderBlock(ref VertexBuffer buffer, Block block, double x, double y, double z);
    }
}
