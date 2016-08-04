using System;

namespace Avocado2D
{
    public class EntityEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entitiy Entity { get; }

        public EntityEventArgs(Entitiy entity)
        {
            Entity = entity;
        }
    }
}