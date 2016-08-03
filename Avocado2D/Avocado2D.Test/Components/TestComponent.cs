using Avocado2D.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Test.Components
{
    [RequiredComponent(typeof(Rotator))]
    public class TestComponent : Component
    {
        public int Health { get; set; }
    }
}