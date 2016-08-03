namespace Avocado2D.Test.Components
{
    [RequiredComponent(typeof(Rotator))]
    public class TestComponent : Component
    {
        public int Health { get; set; }

        public override void OnInitialize()
        {
            base.OnInitialize();
        }
    }
}