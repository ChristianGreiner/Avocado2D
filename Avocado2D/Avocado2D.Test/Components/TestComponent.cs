using Avocado2D.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Test.Components
{
    public class TestComponent : Component
    {
        public int Health { get; set; }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameObject.Transform.Move(40, 40);
            GameObject.Transform.Rotation += 10f * delta;
        }
    }
}