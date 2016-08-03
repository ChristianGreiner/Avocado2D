using Microsoft.Xna.Framework;
using System;

namespace Avocado2D.Test.Components
{
    public class Rotator : Behavior
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameObject.Transform.Rotation += 10f * delta;
        }
    }
}