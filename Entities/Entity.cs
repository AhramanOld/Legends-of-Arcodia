using Arcodia.Worlds;
using OpenTK;
using System;

namespace Arcodia.Entities
{
    public abstract class Entity
    {
        public double PosX;
        public double PosY;
        public double PosZ;

        public double LastTickPosX;
        public double LastTickPosY;
        public double LastTickPosZ;

        public double PrevPosX;
        public double PrevPosY;
        public double PrevPosZ;

        public double MotionX;
        public double MotionY;
        public double MotionZ;

        public double RotationPitch;
        public double RotationYaw;
        public double RotationRoll;

        public World WorldObj;

        protected Entity(World world)
        {
            this.WorldObj = world;
        }

        #region Updating

        public virtual void OnUpdate()
        {
            this.LastTickPosX = PosX;
            this.LastTickPosY = PosY;
            this.LastTickPosZ = PosZ;

            this.OnEntityUpdate();
        }

        public virtual void OnEntityUpdate() {}

        #endregion
        #region Position & Movement

        public void SetHeading(double strafe, double forward, double friction)
        {
            double dist = strafe * strafe + forward * forward;

            if (dist >= 10E-03F)
            {
                dist = Math.Sqrt(dist);
                dist = dist > 1.0D ? dist : 1.0D;
                dist = friction / dist;
                strafe *= dist;
                forward *= dist;

                double sin = Math.Sin(this.RotationYaw * Math.PI / 180.0D);
                double cos = Math.Cos(this.RotationYaw * Math.PI / 180.0D);

                this.MotionX += strafe * cos - forward * sin;
                this.MotionZ += forward * cos + strafe * sin;
            }
        }

        public void MoveEntity(double x, double y, double z)
        {
            this.PosX += x;
            this.PosY += y;
            this.PosZ += z;
        }

        public Vector3 GetRenderPos(float time)
        {
            double x = this.LastTickPosX + (this.PosX - this.LastTickPosX) * time;
            double y = this.LastTickPosY + (this.PosY - this.LastTickPosY) * time;
            double z = this.LastTickPosZ + (this.PosZ - this.LastTickPosZ) * time;

            return new Vector3((float)x, (float)y, (float)z);
        }

        #endregion
        #region Rotation

        public void RotateHeading(double pitch, double yaw)
        {
            this.RotationYaw -= yaw * 0.15D;
            this.RotationPitch += pitch * 0.15D;

            this.RotationPitch = this.RotationPitch < -89.0D ? -89.0D : this.RotationPitch > 89.0D ? 89.0D : this.RotationPitch;
        }

        public Vector3 GetRotation()
        {
            double pitch = this.RotationPitch * Math.PI / 180.0D;
            double yaw = this.RotationYaw * Math.PI / 180.0D;

            return new Vector3((float)(Math.Cos(pitch) * Math.Cos(yaw)), (float)Math.Sin(pitch), (float)(Math.Cos(pitch) * Math.Sin(yaw)));
        }

        #endregion
    }
}