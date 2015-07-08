using Arcodia.Util;
using OpenTK.Input;

namespace Arcodia.Entities.Living.Player
{
    public class MovementHandler
    {
        public float Forward;
        public float Strafe;

        public bool IsJumping;
        public bool IsSneaking;

        public void Update()
        {
            this.Forward = 0.0F;
            this.Strafe = 0.0F;

            var state = Keyboard.GetState();

            if (state[Key.W])
            {
                this.Strafe += 1.0F;
            }

            if (state[Key.S])
            {
                this.Strafe -= 1.0F;
            }

            if (state[Key.D])
            {
                this.Forward += 1.0F;
            }

            if (state[Key.A])
            {
                this.Forward -= 1.0F;
            }

            this.IsJumping = state[Key.Space];
            this.IsSneaking = state[Key.LShift];
        }
    }

    public class RotationHandler
    {
        public int DeltaX;
        public int DeltaY;

        public void Update()
        {
            this.DeltaX = MouseHelper.DeltaX;
            this.DeltaY = MouseHelper.DeltaY;
        }
    }
}