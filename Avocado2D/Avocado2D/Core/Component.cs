using Microsoft.Xna.Framework;
using System;

namespace Avocado2D
{
    public class Component : IDisposable
    {
        /// <summary>
        /// Gets or sets the gameobject of the component.
        /// </summary>
        public GameObject GameObject { get; set; }

        /// <summary>
        /// Whether or not the component is initialized.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Whether or not the component is enabled.
        /// </summary>
        public bool Enabled { get; set; }

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
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
            Initialized = true;
            Enabled = true;
        }

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Gets called when the component gets removed from the gameobject.
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// Removes the component from the gameobject.
        /// </summary>
        public void Dispose()
        {
            GameObject?.RemoveComponent(GetType());
        }
    }
}