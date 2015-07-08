using OpenTK.Graphics.OpenGL4;
using System;

namespace Arcodia.Renderer
{
    public class VertexBuffer : IDisposable
    {
        private bool HasChanged;

        private uint BufferId = 0;
        private uint VertexArrayId = 0;

        private int BufferLength;
        private int VertexCount = 0;

        private readonly int DefaultBufferLength;

        private double[] Buffer;

        private double ColorRed = 1.0D;
        private double ColorGreen = 1.0D;
        private double ColorBlue = 1.0D;
        private double ColorAlpha = 1.0D;

        public VertexBuffer() : this(16) {}

        public VertexBuffer(int length)
        {
            this.BufferLength = length;
            this.DefaultBufferLength = length;

            this.Buffer = new double[this.BufferLength * 7];

            GL.GenBuffers(1, out this.BufferId);
            GL.GenVertexArrays(1, out this.VertexArrayId);

            this.HasChanged = true;
        }

        public void SetColor(double red, double green, double blue, double alpha)
        {
            this.ColorRed = red;
            this.ColorGreen = green;
            this.ColorBlue = blue;
            this.ColorAlpha = alpha;
        }

        public void AddVertex(double x, double y, double z)
        {
            if (this.VertexCount + 8 >= this.BufferLength)
            {
                this.BufferLength += 16;

                Array.Resize(ref this.Buffer, this.BufferLength * 7);
            }

            int index = this.VertexCount * 7;

            this.Buffer[index + 0] = x;
            this.Buffer[index + 1] = y;
            this.Buffer[index + 2] = z;
            this.Buffer[index + 3] = this.ColorRed;
            this.Buffer[index + 4] = this.ColorGreen;
            this.Buffer[index + 5] = this.ColorBlue;
            this.Buffer[index + 6] = this.ColorAlpha;

            this.VertexCount++;
            this.HasChanged = true;
        }

        public void Upload()
        {
            if (this.HasChanged)
            {
                GL.BindVertexArray(this.VertexArrayId);

                GL.BindBuffer(BufferTarget.ArrayBuffer, this.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(this.VertexCount * 7 * sizeof(double)), this.Buffer, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Double, false, 7 * sizeof(double), 0);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Double, false, 7 * sizeof(double), 3 * sizeof(double));
                GL.EnableVertexAttribArray(1);

                GL.BindVertexArray(0);

                this.HasChanged = false;
            }
        }

        public void Draw()
        {
            if (this.HasChanged)
            {
                Console.Out.WriteLine("[Warning] Draw() is called before Upload(). THIS MAY CAUSE ERRORS AND/OR CRASHES. CLOSE THE APPLICATION AND INFORM THE AUTHOR!");
                Console.ReadKey();
            }

            GL.BindVertexArray(this.VertexArrayId);
            GL.DrawArrays(PrimitiveType.Triangles, 0, this.VertexCount);            
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArrays(1, ref this.VertexArrayId);
            GL.DeleteBuffers(1, ref this.BufferId);
        }
    }
}