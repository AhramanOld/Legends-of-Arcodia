using Arcodia.Renderer.Blocks;
using System.Collections.Generic;

namespace Arcodia.Blocks
{
    public class Block
    {
        protected double MinX;
        protected double MinY;
        protected double MinZ;
        protected double MaxX;
        protected double MaxY;
        protected double MaxZ;

        public static readonly Dictionary<int, Block> IdToBlockMap = new Dictionary<int, Block>();
        public static readonly Dictionary<string, Block> NameToBlockMap = new Dictionary<string, Block>();

        protected Block()
        {
            this.SetBounds(0.0D, 0.0D, 0.0D, 1.0D, 1.0D, 1.0D);
        }

        #region Render Bounds

        protected void SetBounds(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.MinZ = minZ;
            this.MaxX = maxX;
            this.MaxY = maxY;
            this.MaxZ = maxZ;
        }

        public double GetMinX()
        {
            return this.MinX;
        }

        public double GetMinY()
        {
            return this.MinY;
        }

        public double GetMinZ()
        {
            return this.MinZ;
        }

        public double GetMaxX()
        {
            return this.MaxX;
        }

        public double GetMaxY()
        {
            return this.MaxY;
        }

        public double GetMaxZ()
        {
            return this.MaxZ;
        }

        #endregion
        #region Block Model

        public virtual IBlockModel GetBlockModel()
        {
            return new ModelBlockStandard();
        }

        #endregion
        #region Registeration

        public static void RegisterBlocks()
        {
            RegisterBlock(0, "air", new BlockAir());
            RegisterBlock(1, "stone", new Block());
        }

        private static void RegisterBlock(int id, string name, Block block)
        {
            IdToBlockMap.Add(id, block);
            NameToBlockMap.Add(name, block);
        }

        #endregion
    }
}