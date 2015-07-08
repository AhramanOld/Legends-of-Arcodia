using System;

namespace Arcodia.Util
{
    public class Timer
    {
        public int ElapsedTicks;

        private long LastSysTime;

        public float RenderTime;
        public float TimerSpeed = 1.0F;
        public float ElapsedPartialTicks;
        private float TicksPerSecond;

        public Timer(float ticks)
        {
            this.TicksPerSecond = ticks;
            this.LastSysTime = DateTime.Now.Ticks * 100L;
        }

        public void Update()
        {
            long now = DateTime.Now.Ticks * 100L;
            long passed = now - this.LastSysTime;
            this.LastSysTime = now;

            if (passed < 0L)
            {
                passed = 0L;
            }

            if (passed > 1000000000L)
            {
                passed = 1000000000L;
            }

            this.ElapsedPartialTicks += (float)((decimal)passed * (decimal)this.TimerSpeed * (decimal)this.TicksPerSecond / 1000000000M);

            this.ElapsedTicks = (int)this.ElapsedPartialTicks;
            
            if (this.ElapsedTicks > 10)
            {
                this.ElapsedTicks = 10;
            }
            this.ElapsedPartialTicks -= this.ElapsedTicks;

            this.RenderTime = this.ElapsedPartialTicks;
        }
    }
}