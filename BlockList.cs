using Arcodia.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace Arcodia
{
    public static class BlockList
    {
        public static readonly Block Air;
        public static readonly Block Stone;
        
        static BlockList()
        {
            BlockList.Air = Block.NameToBlockMap["air"];
            BlockList.Stone = Block.NameToBlockMap["stone"];
        }

        public static Block GetBlockFromId(int id)
        {
            return Block.IdToBlockMap[id];
        }

        public static Block GetBlockFromName(string name)
        {
            return Block.NameToBlockMap[name];
        }

        public static int GetIdFromBlock(Block block)
        {
            return Block.IdToBlockMap.First(k => k.Value == block).Key;
        }

        public static string GetNameFromBlock(Block block)
        {
            return Block.NameToBlockMap.First(k => k.Value == block).Key;
        }
    }
}