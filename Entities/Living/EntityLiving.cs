using Arcodia.Worlds;

namespace Arcodia.Entities.Living
{
    public abstract class EntityLiving : Entity
    {
        public double MoveStrafe;
        public double MoveForward;

        public bool IsJumping;
        public bool IsSneaking;

        protected EntityLiving(World world) : base(world) {}

        #region Updating

        public override void OnEntityUpdate()
        {
            base.OnEntityUpdate();

            this.OnLivingUpdate();

            if (this.LastTickPosX != this.PosX)
            {
                this.PrevPosX = this.LastTickPosX;
            }

            if (this.LastTickPosY != this.PosY)
            {
                this.PrevPosY = this.LastTickPosY;
            }

            if (this.LastTickPosZ != this.PosZ)
            {
                this.PrevPosZ = this.LastTickPosZ;
            }
        }

        public virtual void OnLivingUpdate()
        {
        }

        #endregion
    }
}