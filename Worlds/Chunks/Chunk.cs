using Arcodia.Blocks;
using Arcodia.Util;

namespace Arcodia.Worlds.Chunks
{
    public class Chunk
    {
        public World WorldObj;
        public ChunkPos Pos;

        private byte[] Blocks;

        public bool NeedsUpdate;

        public Chunk(World world, ChunkPos pos)
        {
            this.WorldObj = world;
            this.Pos = pos;

            this.NeedsUpdate = true;

            this.Blocks = new byte[4096];
        }

        #region Block Access

        public Block GetBlock(int x, int y, int z)
        {
            return BlockList.GetBlockFromId(this.Blocks[x + 16 * (y + 16 * z)]);
        }

        public bool SetBlock(Block block, int x, int y, int z)
        {
            var index = x + 16 * (y + 16 * z);
            var block1 = BlockList.GetBlockFromId(this.Blocks[index]);

            if (block != block1)
            {
                this.Blocks[index] = (byte)BlockList.GetIdFromBlock(block);
                this.NeedsUpdate = true;
            }

            return true;
        }

        #endregion
    }
}