using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Graphics.Viewports
{
    public class DefaultViewportAdapter : ViewportAdapter
    {
        /// <summary>
        /// Gets the virtual width of the viewport.
        /// </summary>
        public override int VirtualWidth => _graphicsDevice.Viewport.Width;

        /// <summary>
        /// Gets the virtual height of the viewport.
        /// </summary>
        public override int VirtualHeight => _graphicsDevice.Viewport.Height;

        /// <summary>
        /// Gets the width of the viewport.
        /// </summary>
        public override int ViewportWidth => _graphicsDevice.Viewport.Width;

        /// <summary>
        /// Gets the height of the viewport.
        /// </summary>
        public override int ViewportHeight => _graphicsDevice.Viewport.Height;

        private readonly GraphicsDevice _graphicsDevice;

        public DefaultViewportAdapter(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Gets the scalematrix of the adapter.
        /// </summary>
        /// <returns>Returns the scalematrix.</returns>
        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}