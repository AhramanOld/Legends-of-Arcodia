using Arcodia.Blocks;
using Arcodia.Worlds;

namespace Arcodia.Renderer.Blocks
{
    public interface IBlockModel
    {
        void SetWorld(World world);

        void SetRenderAllFaces(bool value);

        bool RenderBlock(ref VertexBuffer buffer, Block block, double x, double y, double z);
    }
}