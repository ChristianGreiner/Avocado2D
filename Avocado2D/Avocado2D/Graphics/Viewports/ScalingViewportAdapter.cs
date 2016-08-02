using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Graphics.Viewports
{
    public class ScalingViewportAdapter : ViewportAdapter
    {
        /// <summary>
        /// Gets the virtual width of the viewport.
        /// </summary>
        public override int VirtualWidth { get; }

        /// <summary>
        /// Gets the virtual height of the viewport.
        /// </summary>
        public override int VirtualHeight { get; }

        /// <summary>
        /// Gets the width of the viewport.
        /// </summary>
        public override int ViewportWidth => GraphicsDevice.Viewport.Width;

        /// <summary>
        /// Gets the height of the viewport.
        /// </summary>
        public override int ViewportHeight => GraphicsDevice.Viewport.Height;

        public ScalingViewportAdapter(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight)
            : base(graphicsDevice)
        {
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
        }

        /// <summary>
        /// Gets the scale matrix of the viewport.
        /// </summary>
        /// <returns>Returns the scalematrix.</returns>
        public override Matrix GetScaleMatrix()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}