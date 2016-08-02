using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D
{
    public class DrawableComponent : Component
    {
        /// <summary>
        /// Determines if the component is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Determines the draworder of the component.
        /// </summary>
        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                drawOrder = value;
                GameObject?.ComponentDrawOrderChanged.Invoke(this, new ComponentEventArgs(this));
            }
        }

        private int drawOrder;

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}