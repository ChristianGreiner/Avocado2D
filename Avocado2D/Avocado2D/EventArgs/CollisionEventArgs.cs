using Avocado2D.Physics.Collision;
using System;

namespace Avocado2D
{
    public class CollisionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the collisiondata.
        /// </summary>
        public CollisionData CollisionData { get; }

        public CollisionEventArgs(CollisionData collision)
        {
            CollisionData = collision;
        }
    }
}