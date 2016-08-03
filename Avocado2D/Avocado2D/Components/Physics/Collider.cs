using Avocado2D.Physics.Collision;
using Avocado2D.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Avocado2D.Components
{
    public class Collider : Behavior
    {
        /// <summary>
        /// Gets the bounds (rectangle) of the collider.
        /// </summary>
        public RectangleF Bounds { get; private set; }

        /// <summary>
        /// Determines if the collider is static or not.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Gets or sets the local offset of the collider.
        /// </summary>
        public Vector2 Offset { get; set; }

        private int width;
        private int height;

        public override void OnInitialize()
        {
            base.OnInitialize();
            Bounds = new RectangleF((Offset.X + GameObject.Transform.Position.X), (Offset.Y + GameObject.Transform.Position.Y), width, height);
        }

        /// <summary>
        /// Sets the size of the colliders.
        /// </summary>
        /// <param name="width">The width of the collider.</param>
        /// <param name="height">The height of the collider.</param>
        public void SetBounds(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Bounds = new RectangleF((Offset.X + GameObject.Transform.Position.X), (Offset.Y + GameObject.Transform.Position.Y), width, height);
        }
    }
}