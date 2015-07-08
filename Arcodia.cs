using Arcodia.Blocks;
using Arcodia.Entities.Living.Player;
using Arcodia.Renderer;
using Arcodia.Util;
using Arcodia.Worlds;
using System;

namespace Arcodia
{
    public class Arcodia
    {
        public Timer Timer = new Timer(20.0F);
        public Window Window;

        public RenderGlobal RenderGlobal;

        public World TheWorld;
        public EntityPlayer ThePlayer;

        private static Arcodia Instance;

        public static Arcodia GetInstance()
        {
            return Instance;
        }

        public Arcodia()
        {
            Instance = this;

            Block.RegisterBlocks();
        }

        public void Run()
        {
            Console.Title = "Legends of Arcodia Console";

            this.TheWorld = new World();
            this.ThePlayer = new EntityPlayer(this.TheWorld);
            this.TheWorld.SpawnEntityInWorld(this.ThePlayer);

            this.Window = new Window(GetInstance());

            this.RenderGlobal = new RenderGlobal(GetInstance());

            this.Window.Run();
        }

        public void RunGameLoop()
        {
            this.Timer.Update();

            for (int i = 0; i < this.Timer.ElapsedTicks; i++)
            {
                this.RunTick();
            }
        }

        private void RunTick()
        {
            if (this.TheWorld != null)
            {
                this.TheWorld.UpdateWorld();
            }

            MouseHelper.Update(!this.Window.CursorVisible);
        }
    }
}