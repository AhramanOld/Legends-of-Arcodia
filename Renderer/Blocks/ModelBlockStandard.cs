using Arcodia.Blocks;

namespace Arcodia.Renderer.Blocks
{
    public class ModelBlockStandard : ModelBlockBase
    {
        protected double RenderMinX;
        protected double RenderMinY;
        protected double RenderMinZ;
        protected double RenderMaxX;
        protected double RenderMaxY;
        protected double RenderMaxZ;

        protected void SetBlockBounds(Block block)
        {
            this.RenderMinX = block.GetMinX();
            this.RenderMinY = block.GetMinY();
            this.RenderMinZ = block.GetMinZ();
            this.RenderMaxX = block.GetMaxX();
            this.RenderMaxY = block.GetMaxY();
            this.RenderMaxZ = block.GetMaxZ();
        }

        public override bool RenderBlock(ref VertexBuffer buffer, Block block, double x, double y, double z)
        {
            this.SetBlockBounds(block);

            if (this.ShouldRenderAllFaces)
            {
                this.RenderFacePosX(ref buffer, x, y, z);
                this.RenderFaceNegX(ref buffer, x, y, z);
                this.RenderFacePosY(ref buffer, x, y, z);
                this.RenderFaceNegY(ref buffer, x, y, z);
                this.RenderFacePosZ(ref buffer, x, y, z);
                this.RenderFaceNegZ(ref buffer, x, y, z);
            }
            else
            {
                //TODO Remove unnecessary faces
                this.RenderFacePosX(ref buffer, x, y, z);
                this.RenderFaceNegX(ref buffer, x, y, z);
                this.RenderFacePosY(ref buffer, x, y, z);
                this.RenderFaceNegY(ref buffer, x, y, z);
                this.RenderFacePosZ(ref buffer, x, y, z);
                this.RenderFaceNegZ(ref buffer, x, y, z);
            }

            return true;
        }

        public void RenderFacePosX(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMaxX;
            double d2 = y + this.RenderMinY;
            double d3 = y + this.RenderMaxY;
            double d4 = z + this.RenderMinZ;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d1, d3, d5);
            buffer.AddVertex(d1, d3, d4);
            buffer.AddVertex(d1, d2, d5);

            buffer.AddVertex(d1, d2, d4);
            buffer.AddVertex(d1, d2, d5);
            buffer.AddVertex(d1, d3, d4);
        }


        public void RenderFaceNegX(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMinX;
            double d2 = y + this.RenderMinY;
            double d3 = y + this.RenderMaxY;
            double d4 = z + this.RenderMinZ;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d1, d3, d4);
            buffer.AddVertex(d1, d3, d5);
            buffer.AddVertex(d1, d2, d4);

            buffer.AddVertex(d1, d2, d5);
            buffer.AddVertex(d1, d2, d4);
            buffer.AddVertex(d1, d3, d5);
        }

        public void RenderFacePosY(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMinX;
            double d2 = x + this.RenderMaxX;
            double d3 = y + this.RenderMaxY;
            double d4 = z + this.RenderMinZ;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d1, d3, d4);
            buffer.AddVertex(d2, d3, d4);
            buffer.AddVertex(d1, d3, d5);

            buffer.AddVertex(d2, d3, d5);
            buffer.AddVertex(d2, d3, d4);
            buffer.AddVertex(d1, d3, d5);
        }

        public void RenderFaceNegY(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMinX;
            double d2 = x + this.RenderMaxX;
            double d3 = y + this.RenderMinY;
            double d4 = z + this.RenderMinZ;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d2, d3, d4);
            buffer.AddVertex(d1, d3, d4);
            buffer.AddVertex(d2, d3, d5);

            buffer.AddVertex(d1, d3, d5);
            buffer.AddVertex(d2, d3, d5);
            buffer.AddVertex(d2, d3, d4);
        }

        public void RenderFacePosZ(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMinX;
            double d2 = x + this.RenderMaxX;
            double d3 = y + this.RenderMinY;
            double d4 = y + this.RenderMaxY;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d1, d4, d5);
            buffer.AddVertex(d2, d3, d5);
            buffer.AddVertex(d1, d4, d5);

            buffer.AddVertex(d2, d3, d5);
            buffer.AddVertex(d1, d3, d5);
            buffer.AddVertex(d2, d4, d5);
        }

        public void RenderFaceNegZ(ref VertexBuffer buffer, double x, double y, double z)
        {
            double d1 = x + this.RenderMinX;
            double d2 = x + this.RenderMaxX;
            double d3 = y + this.RenderMinY;
            double d4 = y + this.RenderMaxY;
            double d5 = z + this.RenderMaxZ;

            buffer.AddVertex(d2, d4, d5);
            buffer.AddVertex(d1, d4, d5);
            buffer.AddVertex(d2, d3, d5);

            buffer.AddVertex(d1, d3, d5);
            buffer.AddVertex(d2, d3, d5);
            buffer.AddVertex(d1, d4, d5);
        }
    }
}