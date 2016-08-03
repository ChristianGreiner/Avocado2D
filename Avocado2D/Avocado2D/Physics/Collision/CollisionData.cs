using Avocado2D.Components;

namespace Avocado2D.Physics.Collision
{
    public class CollisionData
    {
        /// <summary>
        /// The incoming collider involved in the collision.
        /// </summary>
        public Collider Collider { get; }

        /// <summary>
        /// Gets the side wich the collision took place.
        /// </summary>
        public CollisionContact Contact { get; }

        public CollisionData(Collider collider, CollisionContact contact)
        {
            Collider = collider;
            Contact = contact;
        }
    }
}