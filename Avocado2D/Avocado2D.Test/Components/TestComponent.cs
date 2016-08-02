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

        private float time = 2;

        public override void Update(GameTime gameTime)
        {
            GameObject.Transform.Move(40, 40);

            time -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time <= 2)
            {
                GameObject.Dispose();
            }

            Health++;
        }
    }
}