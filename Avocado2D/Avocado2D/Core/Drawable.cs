using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D
{
    public class Drawable : Component
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
                Entity?.ComponentDrawOrderChanged.Invoke(this, new ComponentEventArgs(this));
            }
        }

        private int drawOrder;

        public Drawable()
        {
            Visible = true;
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}