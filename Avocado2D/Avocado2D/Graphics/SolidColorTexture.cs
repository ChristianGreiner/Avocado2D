using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Avocado2D.Graphics
{
    /// <summary>
    /// Represents a single pixel of a texture.
    /// </summary>
    public class SolidColorTexture : Texture2D
    {
        private Color color;

        /// <summary>
        /// Gets or sets the color of the texture.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                if (value == color) return;
                color = value;
                SetData(new[] { color });
            }
        }

        public SolidColorTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) : base(graphicsDevice, width, height)
        {
            Color[] value = Enumerable.Range(0, width * height).Select(i => color).ToArray();
            SetData(value);
        }

        public SolidColorTexture(GraphicsDevice graphicsDevice)
            : base(graphicsDevice, 1, 1)
        {
            Color = Color.White;
        }

        public SolidColorTexture(GraphicsDevice graphicsDevice, Color color)
            : base(graphicsDevice, 1, 1)
        {
            Color = color;
        }
    }
}