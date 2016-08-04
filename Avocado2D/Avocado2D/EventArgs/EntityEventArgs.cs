using System;

namespace Avocado2D
{
    public class EntityEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; }

        public EntityEventArgs(Entity entity)
        {
            Entity = entity;
        }
    }
}