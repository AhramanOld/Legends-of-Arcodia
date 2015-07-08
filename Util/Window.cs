using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;

namespace Arcodia.Util
{
    public class Window : GameWindow
    {
        private Arcodia Arcodia;

        public Window(Arcodia game) : this(1240, 840, game) {}

        public Window(int width, int height, Arcodia game) : this(width, height, "Legends of Arcodia", game) {}

        public Window(int width, int height, string title, Arcodia game) : base(width, height)
        {
            this.Title = title;
            this.Arcodia = game;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.CursorVisible = false;

            this.Arcodia.RenderGlobal.Init();
            this.Arcodia.RenderGlobal.SetWorld(this.Arcodia.TheWorld);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            this.Arcodia.RunGameLoop();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Escape:
                    this.Exit();
                    break;
                case Key.R:
                    this.CursorVisible = !this.CursorVisible;
                    break;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            this.Arcodia.RenderGlobal.Render();

            this.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            MouseHelper.Resize();

            this.Arcodia.RenderGlobal.Resize();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}