using Arcodia.Worlds;

namespace Arcodia.Entities.Living.Player
{
    public class EntityPlayer : EntityLiving
    {
        private MovementHandler MovementHandler;
        private RotationHandler RotationHandler;

        public EntityPlayer(World world) : base(world)
        {
            this.MovementHandler = new MovementHandler();
            this.RotationHandler = new RotationHandler();
        }

        public override void OnLivingUpdate()
        {
            base.OnLivingUpdate();

            this.MovementHandler.Update();
            this.RotationHandler.Update();

            this.MoveStrafe = this.MovementHandler.Strafe;
            this.MoveForward = this.MovementHandler.Forward;

            this.IsJumping = this.MovementHandler.IsJumping;
            this.IsSneaking = this.MovementHandler.IsSneaking;

            this.MotionY = this.IsJumping ? 0.8D : this.IsSneaking ? -0.8D : this.MotionY;

            this.RotateHeading(this.RotationHandler.DeltaY, this.RotationHandler.DeltaX);

            this.SetHeading(this.MoveStrafe, this.MoveForward, 0.02D);
            this.MoveEntity(this.MotionX, this.MotionY, this.MotionZ);

            this.MotionX *= 0.8D;
            this.MotionY *= 0.8D;
            this.MotionZ *= 0.8D;
        }
    }
}