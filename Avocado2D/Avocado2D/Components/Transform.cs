using Microsoft.Xna.Framework;

namespace Avocado2D.Components
{
    public class Transform : Component
    {
        /// <summary>
        /// Gets or sets the position of the gameobject.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets ther rotation f the gameobject.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale of the gameobject.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Moves the component by a specifiy value.
        /// </summary>
        /// <param name="x">The x-movement.</param>
        /// <param name="y">The y-movement.</param>
        public void Move(float x, float y)
        {
            Position += new Vector2(x * delta, y * delta);
        }

        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = 0f;
            Scale = Vector2.One;
        }

        private float delta;

        /// <summary>
        /// Updates the transform component.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}