using Avocado2D.Components;
using Avocado2D.Physics.Collision;
using Avocado2D.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Avocado2D.Managers
{
    public class CollisionManager : Manager
    {
        private readonly CollisionGrid grid;
        private readonly HashSet<Collider> colliders;

        public CollisionManager(Scene scene) : base(scene)
        {
            grid = new CollisionGrid(2000, 1500, 256);

            colliders = new HashSet<Collider>();

            scene.EntityManager.EntityAdded += EntityAdded;
            scene.EntityManager.EntityRemoved += EntityRemoved;
        }

        private void EntityRemoved(object sender, EntityEventArgs entityEventArgs)
        {
            var gameObj = entityEventArgs.Entity;
            if (gameObj.GetComponent<Collider>() == null) return;

            var collider = gameObj.GetComponent<Collider>();
            colliders.Remove(collider);
            grid.RemoveCollider(collider);
        }

        private void EntityAdded(object sender, EntityEventArgs entityEventArgs)
        {
            var gameObj = entityEventArgs.Entity;
            if (gameObj.GetComponent<Collider>() == null) return;

            var collider = gameObj.GetComponent<Collider>();
            colliders.Add(collider);
            grid.AddCollider(collider);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var collider in colliders)
            {
                var result = grid.Query(collider);

                // are theire other colliders in the grid
                if (result.Count > 0)
                {
                    // yep, check for collision:
                    // bruteforece :(
                    foreach (var currentCollider in result)
                    {
                        foreach (var otherCollider in result)
                        {
                            // intersect the collider with another collider?
                            if (currentCollider.Bounds.IntersectsWith(otherCollider.Bounds))
                            {
                                // yep! do you thing..
                                var intersectingRec = RectangleF.Intersect(currentCollider.Bounds, otherCollider.Bounds);

                                var collisionContact = CollisionContact.Top;

                                //todo: resolve collision
                            }
                        }
                    }
                }
            }
        }
    }
}