using Microsoft.Xna.Framework;

namespace Avocado2D
{
    public class Behavior : Component
    {
        /// <summary>
        /// Determines the update order of the component.
        /// </summary>
        public int UpdateOrder
        {
            get { return updateOrder; }
            set
            {
                updateOrder = value;
                GameObject?.ComponentUpdateOrderChanged.Invoke(this, new ComponentEventArgs(this));
            }
        }

        private int updateOrder;

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}