using Arcodia.Entities;
using Arcodia.Worlds.Chunks;
using System.Collections.Generic;

namespace Arcodia.Worlds
{
    public class World
    {
        public WorldProvider Provider;

        private readonly List<Entity> ActiveEntities = new List<Entity>();

        #region World Update

        public void UpdateWorld()
        {
            foreach (var entity in this.ActiveEntities)
            {
                entity.OnUpdate();
            }
        }

        #endregion
        #region Entities

        public bool SpawnEntityInWorld(Entity entity)
        {
            this.ActiveEntities.Add(entity);

            return true;
        }

        #endregion
        #region Chunk Management

        public Chunk GetChunkByBlockCoords(int x, int y, int z)
        {
            return this.GetChunk(x >> 4, y >> 4, z >> 4);
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            return this.Provider.GetChunk(x, y, z);
        }

        #endregion
    }
}