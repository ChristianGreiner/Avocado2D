using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Graphics.Viewports
{
    public abstract class ViewportAdapter
    {
        /// <summary>
        /// Gets the graphicsdevice of the viewportadapter.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the viewport of the viewportadatper.
        /// </summary>
        public Viewport Viewport => GraphicsDevice.Viewport;

        /// <summary>
        /// Gets the virtual width of the viewport.
        /// </summary>
        public abstract int VirtualWidth { get; }

        /// <summary>
        /// Gets the virtual height of the viewport.
        /// </summary>
        public abstract int VirtualHeight { get; }

        /// <summary>
        /// Gets the real width of the viewport.
        /// </summary>
        public abstract int ViewportWidth { get; }

        /// <summary>
        /// Gets the real height of the viewport.
        /// </summary>
        public abstract int ViewportHeight { get; }

        /// <summary>
        /// Gets the scalematrix of the viewport.
        /// </summary>
        /// <returns></returns>
        public abstract Matrix GetScaleMatrix();

        /// <summary>
        /// Gets the bounding rectangle of the viewport.
        /// </summary>
        public Rectangle BoundingRectangle => new Rectangle(0, 0, VirtualWidth, VirtualHeight);

        /// <summary>
        /// Gets the center of the viewport.
        /// </summary>
        public Point Center => BoundingRectangle.Center;

        protected ViewportAdapter(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public Point PointToScreen(Point point)
        {
            return PointToScreen(point.X, point.Y);
        }

        public virtual Point PointToScreen(int x, int y)
        {
            var scaleMatrix = GetScaleMatrix();
            var invertedMatrix = Matrix.Invert(scaleMatrix);
            return Vector2.Transform(new Vector2(x, y), invertedMatrix).ToPoint();
        }

        /// <summary>
        /// Resets the viewportadapter.
        /// </summary>
        public virtual void Reset() { }
    }
}