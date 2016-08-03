using Avocado2D.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Test.Components
{
    [RequiredComponent(typeof(Rotator))]
    public class TestComponent : Behavior
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
        }
    }
}