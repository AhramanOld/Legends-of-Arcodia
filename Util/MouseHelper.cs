using OpenTK.Input;

namespace Arcodia.Util
{
    public static class MouseHelper
    {
        public static int DeltaX;
        public static int DeltaY;

        private static int MiddleX;
        private static int MiddleY;

        public static void Update(bool shouldLock)
        {
            var state = Mouse.GetCursorState();

            DeltaX = MiddleX - state.X;
            DeltaY = MiddleY - state.Y;

            if (shouldLock)
            {
                Mouse.SetPosition(MiddleX, MiddleY);
            }
        }

        public static void Resize()
        {
            MiddleX = Arcodia.GetInstance().Window.X + (Arcodia.GetInstance().Window.Width / 2);
            MiddleY = Arcodia.GetInstance().Window.Y + (Arcodia.GetInstance().Window.Height / 2);
        }
    }
}