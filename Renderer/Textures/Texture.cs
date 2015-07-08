using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Arcodia.Renderer.Textures
{
    public enum TextureMode { Nearest = 9728, Linear = 9729 };

    public class Texture
    {
        private int TextureId = -1;

        private TextureMode Mode;

        private string FilePath = "";

        public Texture(string file, TextureMode mode)
        {
            this.FilePath = file;
            this.Mode = mode;
        }

        public void Load()
        {
            this.Bind();

            var image = new Bitmap("assets/arcodia/textures/" + this.FilePath + ".png");
            var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, this.Mode == TextureMode.Linear ? (int)TextureMinFilter.LinearMipmapLinear : (int)this.Mode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)this.Mode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            this.UnBind();
        }

        public int GetTextureId()
        {
            if (this.TextureId == -1)
            {
                GL.GenTextures(1, out this.TextureId);
            }

            return this.TextureId;
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, this.GetTextureId());
        }

        public void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Delete()
        {
            if (this.TextureId != -1)
            {
                GL.DeleteTextures(1, ref this.TextureId);
                this.TextureId = -1;
            }
        }
    }
}