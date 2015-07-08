namespace Arcodia.Worlds.Chunks
{
    public struct ChunkPos
    {
        public int X;
        public int Y;
        public int Z;

        public ChunkPos(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override bool Equals(object obj)
        {
            return !(obj is ChunkPos) ? false : (ChunkPos)obj == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(ChunkPos left, ChunkPos right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(ChunkPos left, ChunkPos right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }
    }
}